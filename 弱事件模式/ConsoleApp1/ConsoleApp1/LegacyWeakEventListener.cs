using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleApp1
{
    public class LegacyWeakEventListener : IWeakEventListener
    {
        public LegacyWeakEventListener(EventSource source)
        {
            EventManager.AddListener(source, this);//订阅事件
        }

        public void RemoveListener(EventSource source)
        {
            EventManager.RemoveListener(source, this);//接触订阅
        }
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)//事件处理程序
        {
            OnEvent(sender, e);

            return true;
        }

        private void OnEvent(object source, EventArgs args)
        {
            Console.WriteLine("执行事件处理方法.");
        }
        
        ~LegacyWeakEventListener()
        {
            Console.WriteLine("订阅方的终结方法执行.");
        }
    }
}
