using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PublisherTosubscriber
{
    public class EventBus
    {
        public static EventBus Default => new EventBus(); //静态实例,可用于缓存
        
        /// <summary>
        /// Key - 事件源的类型  value ： 事件处理方法所在的类型
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<Type>> _eventAndHandlerMapping;

        public EventBus()
        {
            _eventAndHandlerMapping = new ConcurrentDictionary<Type, List<Type>>();
            MapEventToHandler();
        }

        /// <summary>
        /// 反射 构造 事件源 事件处理方法 映射
        /// </summary>
        private void MapEventToHandler()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsInterface && typeof(IEventHandler).IsAssignableFrom(type))
                {
                    Type handlerInterface = type.GetInterface("IEventHandler`1");//获取实现的泛型接口
                    if (handlerInterface != null)//必须实现IEventHandler<>接口
                    {
                        Type eventDataType = handlerInterface.GetGenericArguments()[0];//获取泛型参数类型

                        if (_eventAndHandlerMapping.ContainsKey(eventDataType))
                        {
                            List<Type> handlerTypes = _eventAndHandlerMapping[eventDataType];
                            handlerTypes.Add(type);//将当前类型加到eventDataType 对应的集合中
                            _eventAndHandlerMapping[eventDataType] = handlerTypes;//多余的步骤
                        }
                        else
                        {
                            var handlerTypes = new List<Type> { type };
                            _eventAndHandlerMapping[eventDataType] = handlerTypes;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 手动注册事件  --订阅
        /// </summary>
        /// <typeparam name="TEventData">事件源</typeparam>
        /// <param name="eventHandler">要注册的事件</param>
        public void Register<TEventData>(Type eventHandler) where TEventData : IEventData
        {
            List<Type> handlerTypes = _eventAndHandlerMapping[typeof(TEventData)];
            if (!handlerTypes.Contains(eventHandler))
            {
                handlerTypes.Add(eventHandler);
                _eventAndHandlerMapping[typeof(TEventData)] = handlerTypes;
            }
        }



        /// <summary>
        /// 手动解除事件 --取消订阅
        /// </summary>
        /// <typeparam name="TEventData">事件源</typeparam>
        /// <param name="eventHandler">要解除的事件</param>
        public void UnRegister<TEventData>(Type eventHandler) where TEventData : IEventData
        {
            List<Type> handlerTypes = _eventAndHandlerMapping[typeof(TEventData)];
            if (handlerTypes.Contains(eventHandler))
            {
                handlerTypes.Remove(eventHandler);
                _eventAndHandlerMapping[typeof(TEventData)] = handlerTypes;
            }
        }

        /// <summary>
        /// 根据事件源触发事件 --发布事件
        /// </summary>
        /// <typeparam name="TEventData">事件源</typeparam>
        /// <param name="eventData">参数</param>
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            List<Type> handlerTypes = _eventAndHandlerMapping[eventData.GetType()];
            if (handlerTypes != null && handlerTypes.Count > 0)
            {
                foreach (Type handler in handlerTypes)
                {
                    MethodInfo methodInfo = handler.GetMethod("HandleEvent");
                    if (methodInfo != null)
                    {
                        object obj = Activator.CreateInstance(handler);
                        methodInfo.Invoke(obj, new object[] { eventData });
                    }
                }
            }

        }
    }
}
