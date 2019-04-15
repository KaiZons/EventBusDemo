using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherTosubscriber
{
    public interface IEventData
    {
        DateTime EventTime { get; set; }

        object EventSource { get; set; }
    }
}
