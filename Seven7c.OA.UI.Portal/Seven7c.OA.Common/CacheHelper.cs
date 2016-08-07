using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.Common
{
    public class CacheHelper
    {
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            //如果是静态的属性的话，如果想让它有注入的值，那么必须先创建一个实例后，才会注入。
              IApplicationContext ctx = ContextRegistry.GetContext();
              var userInfoDal = ctx.GetObject("CacheHelper") as CacheHelper;
        }

        public static void WriteCache(string key,object value,DateTime exp)
        {
            CacheWriter.Set(key, value, exp);
        }
        public static void WriteCache(string key, object value)
        {
            CacheWriter.Set(key, value);
        }
        public static object GetCache(string key)
        {
            return CacheWriter.Get(key);
        }
    }
}
