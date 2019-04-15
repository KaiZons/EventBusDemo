using System;
using Weiz.EventBus;
using Weiz.EventBus.Events;
using Weiz.EventBus.Core;

namespace Weiz.EventBus.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sendEmailHandler = new UserAddedEventHandlerSendEmail();//实例事件处理对象
            var sendMessageHandler = new UserAddedEventHandlerSendMessage();
            var sendRedbagsHandler = new UserAddedEventHandlerSendRedbags();
            Weiz.EventBus.Core.EventBus.Instance.Subscribe<UserGeneratorEvent>(sendEmailHandler);//注册事件
            Weiz.EventBus.Core.EventBus.Instance.Subscribe<UserGeneratorEvent>(sendMessageHandler);
            Weiz.EventBus.Core.EventBus.Instance.Subscribe<OrderGeneratorEvent>(sendRedbagsHandler);
            
            Core.EventBus.Instance.Publish(new Events.UserGeneratorEvent { UserId = Guid.NewGuid() }, CallBack);
            Core.EventBus.Instance.Publish(new Events.OrderGeneratorEvent { OrderId = Guid.NewGuid()}, CallBack);

            System.Console.ReadKey();
        }

        private static void CallBack(Events.OrderGeneratorEvent orderGeneratorEvent, bool result, Exception ex)
        {
            System.Console.WriteLine("用户下单订阅事件执行成功");
        }

        public static void CallBack(Events.UserGeneratorEvent userGenerator, bool result, Exception ex)
        {
            System.Console.WriteLine("用户注册订阅事件执行成功");
        }
    }
}
