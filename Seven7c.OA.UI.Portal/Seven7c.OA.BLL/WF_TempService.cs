using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public partial class WF_TempService:BaseService<WF_Temp>,IWF_TempService
    {
        public int DeleteTemps(List<int> ids)
        {
            foreach (var id in ids)
            {
                var action = dbSession.WF_TempDal.LoadEntities(a => a.Id == id).FirstOrDefault();
                if (action != null)
                {
                    action.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                }

            }
            return dbSession.SaveChanges();
        }
    }
}
