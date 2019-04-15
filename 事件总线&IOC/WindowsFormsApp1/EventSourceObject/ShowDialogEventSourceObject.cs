using EventBus;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourceObject
{
    /// <summary>
    /// 事件源对象，事件发布方将该对象传递给监听方
    /// </summary>
    public class ShowDialogEventSourceObject:BusEventBase
    {
        public ClickInfoView ClickInfo { get; set; }
        
        public Action<bool, ClickInfoView> Callback { get; set; }

        /// <summary>
        /// 事件源对象构造方法
        /// </summary>
        /// <param name="sender">发布方(被监视的对象)</param>
        /// <param name="clickInfo">传递给订阅者的信息(一般是订阅者感兴趣的数据)</param>
        /// <param name="callBack">事件回调</param>
        public ShowDialogEventSourceObject(object sender, ClickInfoView clickInfo, Action<bool, ClickInfoView> callBack) : base(sender)
        {
            ClickInfo = clickInfo;
            Callback = callBack;
        }
    }
}
