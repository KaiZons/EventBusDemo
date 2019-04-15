using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// 验证问题：如果事件源上注册了一个对象的方法，会导致事件源保留一个该对象的引用
        /// 参考CLR 21.3.5
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //EventSource source = new EventSource();//事件的定义和发布方

            //NaiveEventListener listener = new NaiveEventListener(source);//订阅方

            //source.Raise();//触发事件

            //Console.WriteLine("listener(订阅方)已经为null.");
            //listener = null;

            //TriggerGC();

            //source.Raise();

            //Console.WriteLine("source(发布方)已经为null.");
            //source = null;

            //TriggerGC();
            
            EventSource source = new EventSource();//事件的定义和发布方

            LegacyWeakEventListener listener = new LegacyWeakEventListener(source);//订阅方，初始化订阅事件
            listener.RemoveListener(source);//解除订阅
            source.Raise();

            Console.WriteLine("listener(订阅方)已经为null.");
            listener = null;

            TriggerGC();

            source.Raise();

            Console.WriteLine("source(发布方)已经为null.");
            source = null;

            TriggerGC();
            
            Console.ReadKey();
        }
        static void TriggerGC()
        {
            Console.WriteLine("开始垃圾回收.");

            //回收：不可达 且 没有终结器(即析构方法) 的对象
            GC.Collect();
            //等待有终结器的对象执行完终结方法
            GC.WaitForPendingFinalizers();
            //再次回收对象 （有终结器的对象不可达后需要回收两次，“复活”是有终结器对象才会出现的情况，此处不做论述）
            GC.Collect();
            Console.WriteLine("结束垃圾回收.");

        }
    }
}
