using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using Seven7c.OA.Model.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class RoleInfoController : BaseController
    {
        public IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRoleInfos()
        {
            //page:1
            //rows:20
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            short delFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            var data = RoleInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag, true, u => u.Id)
                .Select(u => new { u.Id, u.RoleName, u.SubTime, u.ModifiedOn });//防止循环序列化 


            var result = new { total = total, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.ModifiedOn = DateTime.Now;
            RoleInfoService.Add(roleInfo);

            if (roleInfo.Id <= 0)
            {
                return Content("添加失败!");
            }

            return Content("ok");
        }

        public ActionResult Delete(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
            {
                return Content("删除失败，未选中删除的数据");
            }

            string[] allIds = Ids.Split(',');
            List<int> idList = new List<int>();
            foreach (var id in allIds)
            {
                idList.Add(int.Parse(id));
            }

            RoleInfoService.DeleteRoles(idList);

            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = RoleInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            roleInfo.ModifiedOn = DateTime.Now;
            RoleInfoService.Update(roleInfo);
            return Content("ok");
        }
    }
}
