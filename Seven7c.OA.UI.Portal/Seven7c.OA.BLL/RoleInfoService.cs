using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService
    {
        public int DeleteRoles(List<int> idList)
        {
            foreach (var id in idList)
            {
                var user = dbSession.RoleInfoDal.LoadEntities(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    user.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                }

            }
            return dbSession.SaveChanges();
        }
    }
}
