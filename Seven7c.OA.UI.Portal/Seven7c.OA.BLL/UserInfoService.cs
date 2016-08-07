using Seven7c.OA.DALFactory;
using Seven7c.OA.IBLL;
using Seven7c.OA.IDAL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {

        #region 抽象工厂
        //private IUserInfoDal UserInfoDal;
        //private IDbSession dbSession;
        //public UserInfoService()
        //{
        //    dbSession = DbSessionFactory.GetCurrentDbSession();
        //    UserInfoDal = dbSession.UserInfoDal;
        //} 
        #endregion
        //依赖注入：Spring.net

        public int DeleteUsers(List<int> idList)
        {
            foreach (var id in idList)
            {
                var user = dbSession.UserInfoDal.LoadEntities(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    user.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                }

            }
            return dbSession.SaveChanges();
        }

        public IQueryable<UserInfo> LoadPageUserInfos(Model.Params.UserInfoQueryParam para)
        {
            short delNormal = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            var temp = dbSession.UserInfoDal.LoadEntities(u => u.DelFlag == delNormal);

            if (!string.IsNullOrEmpty(para.SearchMail))
            {
                temp = temp.Where(u => u.Email.Contains(para.SearchMail));
            }
            if(!string.IsNullOrEmpty(para.SearchName))
            {
                temp = temp.Where(u => u.UName.Contains(para.SearchName));
            }

            para.Total = temp.Count();

            return temp.OrderBy(u => u.Id)
                .Skip(para.PageSize * (para.PageIndex - 1))
                .Take(para.PageSize).AsQueryable();
        }

        public bool SetUserRole(int userId, List<int> allSelectedRoleIds)
        {
            var user = dbSession.UserInfoDal.LoadEntities(u => u.Id == userId).FirstOrDefault();
            //清除用户之前的角色。
            user.RoleInfo.Clear();
            //查询所有的选中角色。
            var allRoles = dbSession.RoleInfoDal.LoadEntities(r => allSelectedRoleIds.Contains(r.Id)).ToList();

            foreach (var roleInfo in allRoles)
            {
                user.RoleInfo.Add(roleInfo);
            }
            return dbSession.SaveChanges() > 0;
        }
    }
}
