using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherTosubscriber
{
    class Program
    {
        /* 
         * 1.如何精简步骤
         * 2.如何解除 发布 与 订阅方的依赖 ：
         * 3.如何避免在订阅者中同时处理多个事件逻辑
         */
        static void Main(string[] args)
        {
            FishingRod fishingRod = new FishingRod();
            FishingMan jeff = new FishingMan("周凯凯");
            jeff.FishingRod = fishingRod;
            while (jeff.FishCount < 5)
            {
                jeff.Fishing();
                Console.WriteLine("---------------------");
            }
            Console.ReadKey();
    }
    }
}
