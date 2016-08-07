using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        public int DeleteActions(List<int> idList)
        {
            foreach (var id in idList)
            {
                var action = dbSession.ActionInfoDal.LoadEntities(a => a.Id == id).FirstOrDefault();
                if (action != null)
                {
                    action.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                }

            }
            return dbSession.SaveChanges();
        }
        public bool SetRole(int actionId, List<int> allSelectRolesIds)
        {
            var action = dbSession.ActionInfoDal.LoadEntities(a => a.Id == actionId).FirstOrDefault();
            action.RoleInfo.Clear();

            var allRoles = dbSession.RoleInfoDal.LoadEntities(r => allSelectRolesIds.Contains(r.Id)).ToList();
            foreach (var roleInfo in allRoles)
            {
                action.RoleInfo.Add(roleInfo);
            }
            return dbSession.SaveChanges() > 0;
        }
    }
}
