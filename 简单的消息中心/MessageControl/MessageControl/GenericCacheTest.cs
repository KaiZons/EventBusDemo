using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageControl
{
    public class GenericCacheTest
    {
        public static string Show()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                //builder.AppendLine(GenericCache<int>.GetCache());
                Console.WriteLine(GenericCache<int>.GetCache());
                Thread.Sleep(10);
                //builder.AppendLine(GenericCache<long>.GetCache());
                Console.WriteLine(GenericCache<long>.GetCache());
                Thread.Sleep(10);
                //builder.AppendLine(GenericCache<DateTime>.GetCache());
                Console.WriteLine(GenericCache<DateTime>.GetCache());
                Thread.Sleep(10);
                //builder.AppendLine(GenericCache<string>.GetCache());
                Console.WriteLine(GenericCache<string>.GetCache());
                Thread.Sleep(10);
                //builder.AppendLine(GenericCache<GenericCacheTest>.GetCache());
                Console.WriteLine(GenericCache<GenericCacheTest>.GetCache());
                Thread.Sleep(10);
                
            }
            return builder.ToString();
        }
    }

}
