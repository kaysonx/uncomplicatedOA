using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {

        int DeleteUsers(List<int> idList);

        IQueryable<UserInfo> LoadPageUserInfos(Model.Params.UserInfoQueryParam para);

        bool SetUserRole(int userId, List<int> allSelectedRoleIds);
    }
}
