using Seven7c.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Seven7c.OA.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbSession()
        {
            //IDbSession dbSession = HttpContext.Current.Items["DbSession"] as IDbSession;
            //if (dbSession == null)
            //{
            //    dbSession = new DbSession();
            //    HttpContext.Current.Items.Add("DbSession", dbSession);
            //}


            IDbSession dbSession = CallContext.GetData("DbSession") as IDbSession;
            if (dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("DbSession", dbSession);
            }
            return dbSession;
        }
    }
}
