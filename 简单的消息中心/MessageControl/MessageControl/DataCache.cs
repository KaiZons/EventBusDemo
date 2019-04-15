using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageControl
{
    static class DataCache<TModel>
    {
        private static  IEnumerable<TModel> Cache { get; set; }

        //public static IEnumerable<TModel> GetCacheData<TDataContext>(TDataContext dataContext)
        //        where TDataContext : DataContext
        //{
        //    if (dataContext == null)
        //    {
        //        throw new ArgumentNullException("dataContext");
        //    }
        //    if (CacheData == null)
        //    {
        //        CacheData = dataContext.GetTable<TModel>().ToList();
        //    }
        //    return CacheData;
        //}
    }
}
