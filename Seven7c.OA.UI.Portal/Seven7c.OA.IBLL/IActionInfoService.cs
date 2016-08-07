using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.IBLL
{
    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        int DeleteActions(List<int> ids);

        bool SetRole(int actionId, List<int> allSelectRolesIds);
    }
}
