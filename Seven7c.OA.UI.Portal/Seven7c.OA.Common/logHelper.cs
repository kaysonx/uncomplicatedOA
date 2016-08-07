using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seven7c.OA.Common
{
    public class logHelper
    {
        //异常信息队列
        public static Queue<string> ExcMsg;

        static logHelper()
        {
            ExcMsg = new Queue<string>();
            ThreadPool.QueueUserWorkItem(u =>
                {
                    while (true)
                    {
                        string str = string.Empty;

                        if(ExcMsg == null)
                        {
                            continue;
                        }
                        lock (ExcMsg)
                        {
                            if(ExcMsg.Count()>0)
                            {
                                str = ExcMsg.Dequeue();
                            }
                        }
                        //写日志
                        if(!string.IsNullOrEmpty(str))
                        {
                            ILog log = log4net.LogManager.GetLogger("log");
                            log.Error(str);
                        }
                        if(ExcMsg.Count() <= 0)
                        {
                            Thread.Sleep(30);
                        }
                    }
                });
        }

        public static void WriteLog(string msg)
        {
            lock (ExcMsg)
            {
                ExcMsg.Enqueue(msg);
            }
        }
    }
}
