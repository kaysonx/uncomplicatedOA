 

using Seven7c.OA.IDAL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.EFDAL
{
  
   
	public partial class ActionInfoDal :BaseDal<ActionInfo>,IDAL.IActionInfoDal
    {
    } 
	public partial class MesssageDal :BaseDal<Messsage>,IDAL.IMesssageDal
    {
    } 
	public partial class OrderInfoDal :BaseDal<OrderInfo>,IDAL.IOrderInfoDal
    {
    } 
	public partial class R_UserInfo_ActionInfoDal :BaseDal<R_UserInfo_ActionInfo>,IDAL.IR_UserInfo_ActionInfoDal
    {
    } 
	public partial class RoleInfoDal :BaseDal<RoleInfo>,IDAL.IRoleInfoDal
    {
    } 
	public partial class UserInfoDal :BaseDal<UserInfo>,IDAL.IUserInfoDal
    {
    } 
	public partial class UserInfoExtDal :BaseDal<UserInfoExt>,IDAL.IUserInfoExtDal
    {
    } 
	public partial class WF_InstanceDal :BaseDal<WF_Instance>,IDAL.IWF_InstanceDal
    {
    } 
	public partial class WF_StepDal :BaseDal<WF_Step>,IDAL.IWF_StepDal
    {
    } 
	public partial class WF_TempDal :BaseDal<WF_Temp>,IDAL.IWF_TempDal
    {
    } 

}