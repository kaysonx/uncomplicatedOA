using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class ActionInfoController : BaseController
    {
        public IActionInfoService ActionInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }

        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        //
        // GET: /ActionInfo/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllActionInfos()
        {
            //page:1
            //rows:20
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            short delFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            var data = ActionInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag, true, u => u.Id)
                .Select(u => new {u.ActionName,u.Id,u.Remark,u.SubTime,u.Url,u.HttpMethod,u.ActionInfoType,u.IconUrl});//防止循环序列化 


            var result = new { total = total, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            actionInfo.SubTime = DateTime.Now;

            
            ActionInfoService.Add(actionInfo);

            if (actionInfo.Id <= 0)
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

            ActionInfoService.DeleteActions(idList);

            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = ActionInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {

            ActionInfoService.Update(actionInfo);
            return Content("ok");
        }

        public ActionResult SetActionToRole(int id)
        {
            //拿到待设置的权限id
            var action = ActionInfoService.LoadEntities(a => a.Id == id).FirstOrDefault();
            ViewData.Model = action;
            //拿到所有的角色信息。
            ViewBag.AllRoles = RoleInfoService.LoadEntities(r => r.DelFlag == delNormal).ToList();
            //拿到当前权限已经关联的所有角色id。
            ViewBag.ExistRolesId = action.RoleInfo.Select(r => r.Id).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult SetActionToRole(int id, FormCollection collection)
        {
            int actionId = id;

            List<int> allSelectRolesIds = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if(key.StartsWith("ckb_"))
                {
                    allSelectRolesIds.Add(int.Parse(Request[key]));
                }
            }

            ActionInfoService.SetRole(actionId, allSelectRolesIds);
            return Content("ok");
        }

        //图标上传
        public ActionResult UpLoadIcon()
        {
            if(Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                string virthPath = "/UpLoad/Images/" + Guid.NewGuid().ToString() + file.FileName;
                string name = Server.MapPath(virthPath);
                file.SaveAs(name);

                return Content(virthPath);
            }
            return Content("error");
        }
         
    }
}
