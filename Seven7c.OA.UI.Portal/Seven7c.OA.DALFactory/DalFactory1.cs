 
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
    /// ³éÏó¹¤³§
    /// </summary>
    public partial class DalFactory
    {
   
	
		
		public static IActionInfoDal GetActionInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ActionInfoDal", true);

            return obj as IActionInfoDal;
        }
	
		
		public static IMesssageDal GetMesssageDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".MesssageDal", true);

            return obj as IMesssageDal;
        }
	
		
		public static IOrderInfoDal GetOrderInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".OrderInfoDal", true);

            return obj as IOrderInfoDal;
        }
	
		
		public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".R_UserInfo_ActionInfoDal", true);

            return obj as IR_UserInfo_ActionInfoDal;
        }
	
		
		public static IRoleInfoDal GetRoleInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".RoleInfoDal", true);

            return obj as IRoleInfoDal;
        }
	
		
		public static IUserInfoDal GetUserInfoDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal", true);

            return obj as IUserInfoDal;
        }
	
		
		public static IUserInfoExtDal GetUserInfoExtDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoExtDal", true);

            return obj as IUserInfoExtDal;
        }
	
		
		public static IWF_InstanceDal GetWF_InstanceDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_InstanceDal", true);

            return obj as IWF_InstanceDal;
        }
	
		
		public static IWF_StepDal GetWF_StepDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_StepDal", true);

            return obj as IWF_StepDal;
        }
	
		
		public static IWF_TempDal GetWF_TempDal()
        {
            
            object obj = Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_TempDal", true);

            return obj as IWF_TempDal;
        }
	}

}