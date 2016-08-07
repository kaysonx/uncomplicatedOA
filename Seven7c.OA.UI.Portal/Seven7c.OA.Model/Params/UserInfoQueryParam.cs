using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.Model.Params
{
    public class UserInfoQueryParam:BaseParam
    {
        //封装查询的数据。
        public string SearchName { get; set; }
        public string SearchMail { get; set; }
    }
}
