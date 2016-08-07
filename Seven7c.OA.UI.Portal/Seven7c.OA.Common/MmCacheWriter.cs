using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.Common
{
    public class MmCacheWriter:ICacheWriter
    {
        public static readonly MemcachedClient MemcachedClient;
        static MmCacheWriter()
        {
            if(string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["memcachedServer"]))
            {
                throw new Exception("请在web.config文件中加入Memcached服务配置节点。");
            }

            string[] servers = System.Configuration.ConfigurationManager.AppSettings["memcachedServer"].Split(',');

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            MemcachedClient = new Memcached.ClientLibrary.MemcachedClient();
            MemcachedClient.EnableCompression = false;
        }

        public void Set(string key, object value, DateTime exp)
        {
            MemcachedClient.Set(key, value, exp);
        }

        public void Set(string key, object value)
        {
            MemcachedClient.Set(key, value, DateTime.MaxValue);
        }

        public object Get(string key)
        {
            return MemcachedClient.Get(key);
        }
    }
}
