 

using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
 
	public partial class ActionInfoService :BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.ActionInfoDal;
        }
    }
 
	public partial class MesssageService :BaseService<Messsage>,IMesssageService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.MesssageDal;
        }
    }
 
	public partial class OrderInfoService :BaseService<OrderInfo>,IOrderInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.OrderInfoDal;
        }
    }
 
	public partial class R_UserInfo_ActionInfoService :BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.R_UserInfo_ActionInfoDal;
        }
    }
 
	public partial class RoleInfoService :BaseService<RoleInfo>,IRoleInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.RoleInfoDal;
        }
    }
 
	public partial class UserInfoService :BaseService<UserInfo>,IUserInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.UserInfoDal;
        }
    }
 
	public partial class UserInfoExtService :BaseService<UserInfoExt>,IUserInfoExtService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.UserInfoExtDal;
        }
    }
 
	public partial class WF_InstanceService :BaseService<WF_Instance>,IWF_InstanceService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.WF_InstanceDal;
        }
    }
 
	public partial class WF_StepService :BaseService<WF_Step>,IWF_StepService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.WF_StepDal;
        }
    }
 
	public partial class WF_TempService :BaseService<WF_Temp>,IWF_TempService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = dbSession.WF_TempDal;
        }
    }

}