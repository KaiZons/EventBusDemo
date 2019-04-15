using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static PublisherTosubscriber.EnumLib;

namespace PublisherTosubscriber
{
    /// <summary>
    /// 订阅者
    /// </summary>
    public class FishingRod
    {
        //public delegate void FishingHandler(FishingEventData eventData);

        //public event FishingHandler FishingEvent;
        

        //public FishingRod()
        //{
        //    //Assembly assembly = Assembly.GetExecutingAssembly();
        //    //foreach (Type type in assembly.GetTypes())
        //    //{
        //    //    if (!type.IsInterface && typeof(IEventHandler<FishingEventData>).IsAssignableFrom(type))
        //    //    {
        //    //        var dd = type.GetInterfaces();
        //    //        Type handlerInterface = type.GetInterface("IEventHandler`1");//获取类实现的泛型接口
        //    //        Type eventDataType = handlerInterface.GetGenericArguments()[0];//获取泛型参数
        //    //        if (eventDataType.Equals(typeof(FishingEventData)))
        //    //        {
        //    //            IEventHandler<FishingEventData> handler = Activator.CreateInstance(type) as IEventHandler<FishingEventData>;
        //    //            FishingEvent += handler.HandleEvent;
        //    //        }
        //    //    }
        //    //}
        //}
        
        public void ThrowHook(FishingMan man)
        {
            Console.WriteLine("开始下钩！");
            if (new Random().Next() % 2 == 0)
            {
                var type = (FishType)new Random().Next(0, 5);
                Console.WriteLine("铃铛：叮叮叮，鱼儿咬钩了");
                FishingEventData eventData = new FishingEventData();
                eventData.EventSource = this;
                eventData.FishType = type;
                eventData.FishingMan = man;
                EventBus.Default.Trigger<FishingEventData>(eventData);
            }
        }
    }
}
