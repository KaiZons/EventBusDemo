using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PublisherTosubscriber.EnumLib;

namespace PublisherTosubscriber
{
    public class FishingEventData : EventData
    {
        public FishType FishType { get; set; }
        public FishingMan FishingMan { get; set; }
    }
}
