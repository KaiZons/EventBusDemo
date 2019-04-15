using Common;
using EventSourceObject;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;

namespace EventSubscribers
{
    /// <summary>
    /// 监听器
    /// </summary>
    public class TestButtonClickEventPeers : IBusPeer
    {
        public void Initialize()
        {
            var hub = IoCContainer.Current.Resolve<IBusEventHub>();
            hub.Subscribe<ShowDialogEventSourceObject>(OnClickEventReceived);
        }
        
        /// <summary>
        /// 监听到事件后的事件处理程序
        /// </summary>
        /// <param name="eventObject"></param>
        private void OnClickEventReceived(ShowDialogEventSourceObject eventObject)
        {
            if (MessageBox.Show("您确定要打开新弹窗吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                return;
            }
            using (SubForm dialog = new SubForm(eventObject?.ClickInfo))
            {
                dialog.ShowDialog();
                eventObject?.Callback?.Invoke(true, eventObject?.ClickInfo);
            }
        }
    }
}
