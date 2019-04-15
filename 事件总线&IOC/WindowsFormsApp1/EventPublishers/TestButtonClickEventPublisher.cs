using Common;
using EventBus;
using EventSourceObject;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPublishers
{
    /// <summary>
    /// 发布方
    /// </summary>
    public class TestButtonClickEventPublisher
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="sender">事件源对象成员：发布主体</param>
        /// <param name="clickInfo">事件源对象成员：监听方感兴趣的数据</param>
        /// <param name="clickCallback">事件源对象成员：回调</param>
        public static void PublishMainFormButtonClickBusEvent(object sender, ClickInfoView clickInfo, Action<bool, ClickInfoView> clickCallback)
        {
            //获取具体实例的方法
            //1.new一个具体的IBusEventHub实例，比如：var hub = new BusEventHub();// BusEventHub是IBusEventHub的具体类
            //IOC注册时标明：iocContainer.Register(typeof(IBusEventHub), typeof(BusEventHub))？
            //2.使用IOC生成一个实例。 ==> 面向接口编程
            var hub = IoCContainer.Current.Resolve<IBusEventHub>();
            hub.Publish<ShowDialogEventSourceObject>(new ShowDialogEventSourceObject(sender, clickInfo, clickCallback));
        }
    }
}
