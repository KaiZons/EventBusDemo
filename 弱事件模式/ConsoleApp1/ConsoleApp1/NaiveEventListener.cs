using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class NaiveEventListener
    {
        public NaiveEventListener()
        { }
        public NaiveEventListener(EventSource source)
        {
            source.Raise();
            source.EventHandlerEvent += OnEvent;//此处注册事件，会导致source对象中包含一份当前NaiveEventListener对象的引用
        }

        private void OnEvent(object source, EventArgs args)
        {
            Console.WriteLine("执行事件处理方法.");
        }

        ~NaiveEventListener()
        {
            Console.WriteLine("订阅方的终结方法执行.");
        }

    }
}
