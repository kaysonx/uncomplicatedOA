using Seven7c.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Seven7c.OA.DALFactory
{
    /// <summary>
    /// 抽象工厂
    /// </summary>
    public partial class DalFactory
    {
        public static readonly string AssemblyName;

        static DalFactory()
        {
            AssemblyName = System.Configuration.ConfigurationManager.AppSettings["AssemblyName"];
        }

        #region 反射创建实例
        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    IUserInfoDal userInfoDal = HttpRuntime.Cache[AssemblyName + ".UserInfoDal"] as IUserInfoDal;
        //    if(userInfoDal == null)
        //    {
        //        userInfoDal = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal", true) as IUserInfoDal;
        //        HttpRuntime.Cache.Insert(AssemblyName + ".UserInfoDal", userInfoDal);
        //    }
        //    return userInfoDal;
        //}

        //public static IOrderInfoDal GetOrderInfoDal()
        //{
        //    IOrderInfoDal orderInfoDal = HttpRuntime.Cache[AssemblyName + ".OrderInfoDal"] as IOrderInfoDal;
        //    if (orderInfoDal == null)
        //    {
        //        orderInfoDal = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".OrderInfoDal", true) as IOrderInfoDal;
        //        HttpRuntime.Cache.Insert(AssemblyName + ".OrderInfoDal", orderInfoDal);
        //    }
        //    return orderInfoDal;
        //} 
        #endregion
    }
}
