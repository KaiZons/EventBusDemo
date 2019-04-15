using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GCCustom
    {
        /// <summary>
        /// 强制回收
        /// </summary>
        //static void TriggerGC()
        //{
        //    //回收：不可达 且 没有终结器(即析构方法) 的对象
        //    GC.Collect();
        //    //等待有终结器的对象执行完终结方法
        //    GC.WaitForPendingFinalizers();
        //    //再次回收对象 （有终结器的对象不可达后需要回收两次，“复活”是有终结器对象才会出现的情况，此处不做论述）
        //    GC.Collect();
        //}
    }
}
