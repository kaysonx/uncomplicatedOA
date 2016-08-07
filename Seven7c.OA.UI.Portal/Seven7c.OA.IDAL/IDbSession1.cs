 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.IDAL
{
  
    public partial interface IDbSession
    { 
	
		IActionInfoDal ActionInfoDal { get; }
	
		IMesssageDal MesssageDal { get; }
	
		IOrderInfoDal OrderInfoDal { get; }
	
		IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal { get; }
	
		IRoleInfoDal RoleInfoDal { get; }
	
		IUserInfoDal UserInfoDal { get; }
	
		IUserInfoExtDal UserInfoExtDal { get; }
	
		IWF_InstanceDal WF_InstanceDal { get; }
	
		IWF_StepDal WF_StepDal { get; }
	
		IWF_TempDal WF_TempDal { get; }
	}

}