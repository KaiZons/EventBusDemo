using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PublisherTosubscriber.EnumLib;

namespace PublisherTosubscriber
{
    public class FishingMan
    {
        public FishingMan(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int FishCount { get; set; }

        public FishingRod FishingRod { get; set; }

        public void Fishing()
        {
            this.FishingRod.ThrowHook(this);
        }
        
        public void Update(FishingEventData data)
        {
            FishCount++;
            Console.WriteLine("{0}：钓到一条[{2}]，已经钓到{1}条鱼了！", Name, FishCount, data.FishType);
        }
    }
}
