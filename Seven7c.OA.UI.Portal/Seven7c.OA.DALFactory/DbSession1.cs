 
using Seven7c.OA.EFDAL;
using Seven7c.OA.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.DALFactory
{
    public partial class DbSession :IDbSession
    {
   
	
		private IActionInfoDal _ActionInfoDal;
		public IActionInfoDal ActionInfoDal
		{
			get
			{
				if (_ActionInfoDal == null)
				{
					_ActionInfoDal = DalFactory.GetActionInfoDal();
				}
				return _ActionInfoDal;
			}
		}
	
		private IMesssageDal _MesssageDal;
		public IMesssageDal MesssageDal
		{
			get
			{
				if (_MesssageDal == null)
				{
					_MesssageDal = DalFactory.GetMesssageDal();
				}
				return _MesssageDal;
			}
		}
	
		private IOrderInfoDal _OrderInfoDal;
		public IOrderInfoDal OrderInfoDal
		{
			get
			{
				if (_OrderInfoDal == null)
				{
					_OrderInfoDal = DalFactory.GetOrderInfoDal();
				}
				return _OrderInfoDal;
			}
		}
	
		private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfoDal;
		public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
		{
			get
			{
				if (_R_UserInfo_ActionInfoDal == null)
				{
					_R_UserInfo_ActionInfoDal = DalFactory.GetR_UserInfo_ActionInfoDal();
				}
				return _R_UserInfo_ActionInfoDal;
			}
		}
	
		private IRoleInfoDal _RoleInfoDal;
		public IRoleInfoDal RoleInfoDal
		{
			get
			{
				if (_RoleInfoDal == null)
				{
					_RoleInfoDal = DalFactory.GetRoleInfoDal();
				}
				return _RoleInfoDal;
			}
		}
	
		private IUserInfoDal _UserInfoDal;
		public IUserInfoDal UserInfoDal
		{
			get
			{
				if (_UserInfoDal == null)
				{
					_UserInfoDal = DalFactory.GetUserInfoDal();
				}
				return _UserInfoDal;
			}
		}
	
		private IUserInfoExtDal _UserInfoExtDal;
		public IUserInfoExtDal UserInfoExtDal
		{
			get
			{
				if (_UserInfoExtDal == null)
				{
					_UserInfoExtDal = DalFactory.GetUserInfoExtDal();
				}
				return _UserInfoExtDal;
			}
		}
	
		private IWF_InstanceDal _WF_InstanceDal;
		public IWF_InstanceDal WF_InstanceDal
		{
			get
			{
				if (_WF_InstanceDal == null)
				{
					_WF_InstanceDal = DalFactory.GetWF_InstanceDal();
				}
				return _WF_InstanceDal;
			}
		}
	
		private IWF_StepDal _WF_StepDal;
		public IWF_StepDal WF_StepDal
		{
			get
			{
				if (_WF_StepDal == null)
				{
					_WF_StepDal = DalFactory.GetWF_StepDal();
				}
				return _WF_StepDal;
			}
		}
	
		private IWF_TempDal _WF_TempDal;
		public IWF_TempDal WF_TempDal
		{
			get
			{
				if (_WF_TempDal == null)
				{
					_WF_TempDal = DalFactory.GetWF_TempDal();
				}
				return _WF_TempDal;
			}
		}
	}

}