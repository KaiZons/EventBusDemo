using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class EventSource
    {
        /// <summary>
        /// 定义事件 并初始化使其不为null
        /// </summary>
        public event EventHandler EventHandlerEvent = delegate { };

        /// <summary>
        /// 触发事件
        /// </summary>
        public void Raise()
        {
            EventHandlerEvent(this, EventArgs.Empty);
        }
    }
}
