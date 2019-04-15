﻿/****************************************************************
** 文件名:   EventBus.cs
** 主要类:   EventBus类  
** Copyright (c) 章为忠
** 创建人:   
** 日  期:   2017.3.10
** 修改人:   
** 日  期:   
** 修改内容： 
** 描  述:  
** 版  本:   
** 备  注:   
****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Weiz.EventBus.Core
{
    public class EventBus
    {
        /// <summary>
        /// 事件总线对象
        /// </summary>
        private static EventBus _eventBus = null;

        /// <summary>
        /// 领域模型事件句柄字典，用于存储领域模型的句柄
        /// key-事件源 value-包含事件处理方法的对象
        /// </summary>
        private static Dictionary<Type, List<object>> _dicEventHandler = new Dictionary<Type, List<object>>();

        /// <summary>
        /// 附加领域模型处理句柄时，锁住
        /// </summary>
        private readonly object _syncObject = new object();

        /// <summary>
        /// 单例事件总线
        /// </summary>
        public static EventBus Instance
        {
            get
            {
                return _eventBus ?? (_eventBus = new EventBus());
            }
        }

        public EventBus()
        {
            InstanceForXml();
        }

        /// <summary>
        /// 通过ＸＭＬ文件初始化事件总线，订阅信自在ＸＭＬ里配置
        /// </summary>
        /// <returns></returns>
        private static void InstanceForXml()
        {
            //MapEventToHandler();
            //XElement root = XElement.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EventBus.xml"));
            //foreach (var evt in root.Elements("Event"))
            //{
            //    List<object> handlers = new List<object>();

            //    Type publishEventType = Type.GetType(evt.Element("PublishEvent").Value);
            //    foreach (var subscritedEvt in evt.Elements("SubscribedEvents"))
            //    {
            //        foreach (var concreteEvt in subscritedEvt.Elements("SubscribedEvent"))
            //        {
            //            var type = Type.GetType(concreteEvt.Value);
            //            handlers.Add(System.Activator.CreateInstance(type));

            //        }
            //        _dicEventHandler[publishEventType] = handlers;
            //    }
            //}
        }

        /// <summary>
        /// 反射 构造 事件源 事件处理方法 映射
        /// </summary>
        private static void MapEventToHandler()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();//获取当前正在执行的程序集
            foreach (Type type in assembly.GetTypes())
            {
                //具体实现类
                if (type.IsInterface || type.IsGenericType)
                {
                    continue;
                }

                //判断是否实现了IEventHandler<>泛型接口， 即是不是事件处理方法
                if (!Array.Exists(type.GetInterfaces(), a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                {
                    continue;
                }

                //获取泛型接口
                foreach (Type handlerInterface in type.GetInterfaces())
                {
                    if (!handlerInterface.IsGenericType || handlerInterface.Name != "IEventHandler`1")
                    {
                        continue;
                    }
                    if (handlerInterface == null)
                    {
                        continue;
                    }

                    //获取事件源类型
                    Type eventDataType = handlerInterface.GetGenericArguments()[0];
                    
                    if (_dicEventHandler.ContainsKey(eventDataType))
                    {
                        _dicEventHandler[eventDataType].Add(Activator.CreateInstance(type));
                    }
                    else
                    {
                        _dicEventHandler[eventDataType] = new List<object>() { Activator.CreateInstance(type) };
                    }
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            return o1Type == o2Type;
        };

        #region 订阅事件

        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            //同步锁
            lock (_syncObject)//可换成线程安全的字典ConcurrentDictionary
            {
                //获取领域模型的类型
                var eventType = typeof(TEvent);
                //如果此领域类型在事件总线中已注册过
                if (_dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = _dicEventHandler[eventType];
                    if (handlers != null)
                    {
                        handlers.Add(eventHandler);
                    }
                    else
                    {
                        handlers = new List<object>
                        {
                            eventHandler
                        };
                    }
                }
                else
                {
                    _dicEventHandler.Add(eventType, new List<object> { eventHandler });
                }
            }
        }

        /// <summary>
        /// 订阅事件实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        public void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : IEvent
        {
            //将方法体提取成Action，通过Action构造一个实现了IEventHandler接口的对象
            Subscribe<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));
        }
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : IEvent
        {
            foreach (var eventHandler in eventHandlers)
            {
                Subscribe<TEvent>(eventHandler);
            }
        }

        #endregion

        #region 发布事件

        public void Publish<TEvent>(TEvent tEvent, Action<TEvent, bool, Exception> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (_dicEventHandler.ContainsKey(eventType) && _dicEventHandler[eventType] != null &&
                _dicEventHandler[eventType].Count > 0)
            {
                var handlers = _dicEventHandler[eventType];
                try
                {
                    foreach (var handler in handlers)
                    {
                        var eventHandler = handler as IEventHandler<TEvent>;
                        eventHandler.Handle(tEvent);
                        callback(tEvent, true, null);//回调是不是应该放在循环外面
                    }
                }
                catch (Exception ex)
                {
                    callback(tEvent, false, ex);
                }
            }
            else
            {
                callback(tEvent, false, null);
            }
        }

        #endregion

        #region 取消订阅
        /// <summary>
        /// 取消订阅事件 （只要类型相同，就能取消订阅）
        /// </summary>
        /// <param name="type">事件源</param>
        /// <param name="subType">事件类型对象</param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            lock (_syncObject)
            {
                var eventType = typeof(TEvent);
                if (_dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = _dicEventHandler[eventType];
                    if (handlers != null
                        && handlers.Exists(deh => eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        #endregion

    }
}
