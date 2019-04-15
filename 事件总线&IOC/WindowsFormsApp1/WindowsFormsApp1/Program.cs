using Common;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Views;

namespace EventBusIOCEntry
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializerIOC();
            InitializeEventBusPeers();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// 注册IOC
        /// </summary>
        private static void InitializerIOC()
        {
            IoCContainer iocContainer = IoCContainer.Current;
            iocContainer.Register(typeof(IBusEventHub), typeof(BusEventHub));
        }

        /// <summary>
        /// 注册所有监视器
        /// </summary>
        private static void InitializeEventBusPeers()
        {
            Assembly assembly = Assembly.Load("EventSubscribers");//为了方便获取程序集，需要在程序入口程序集中添加监听器的引用
            var types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsClass && typeof(IBusPeer).IsAssignableFrom(type))
                {
                    var peer = Activator.CreateInstance(type) as IBusPeer;
                    peer.Initialize();//注册监视器
                }
            }
        }
    }
}
