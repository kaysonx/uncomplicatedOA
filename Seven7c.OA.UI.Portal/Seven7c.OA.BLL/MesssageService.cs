using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.BLL
{
    public partial class MesssageService:BaseService<Messsage>,IMesssageService
    {
        public int DeleteMessages(List<int> idList, string flag)
        {
            foreach (var id in idList)
            {
                var action = dbSession.MesssageDal.LoadEntities(m => m.Id == id).FirstOrDefault();
                if (action != null)
                {
                    if(flag == "get")
                    {
                        action.DelFlagGet = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                    }
                    else if(flag == "send")
                    {
                        action.DelFlagSend = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Deleted;
                    }
                    
                }

            }
            return dbSession.SaveChanges();
        }
    }
}
