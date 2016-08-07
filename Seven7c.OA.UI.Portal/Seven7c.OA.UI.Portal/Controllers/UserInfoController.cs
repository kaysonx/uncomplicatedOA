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
    public class UserInfoController : BaseController
    {
        //
        IUserInfoService UserInfoService { get; set; }
        IRoleInfoService RoleInfoService { get; set; }
        IActionInfoService ActionInfoService { get; set; }

        IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }

        short delNormal = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            return View();
        }


        #region 加载用户信息
        public ActionResult GetAllUserInfos()
        {
            //page:1
            //rows:20
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;

            string searchName = Request["searchName"];
            string searchMail = Request["searchMail"];

            var para = new UserInfoQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = total,
                SearchName = searchName,
                SearchMail = searchMail
            };

            var data = UserInfoService.LoadPageUserInfos(para).Select(u => new { u.Id, u.UName, u.DelFlag, u.Email, u.Pwd, u.SubTime, u.Remark }).ToList();
            #region Old
            //short delFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            //var data = UserInfoService.LoadPageEntities(pageSize, pageIndex, out total, u => u.DelFlag == delFlag, true, u => u.Id)
            //    .Select(u => new { u.Id, u.UName, u.Pwd, u.SubTime, u.Remark, u.Email });//防止循环序列化 
            #endregion

            var result = new { total = para.Total, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 添加
        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.DelFlag = (short)Seven7c.OA.Model.Enum.DelFlagEnum.Normal;
            userInfo.SubTime = DateTime.Now;

            UserInfoService.Add(userInfo);

            if (userInfo.Id <= 0)
            {
                return Content("添加失败!");
            }

            return Content("ok");
        }
        #endregion

        #region 删除
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

            UserInfoService.DeleteUsers(idList);

            return Content("ok");
        }
        #endregion

        #region 编辑
        public ActionResult Edit(int id)
        {
            ViewData.Model = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            userInfo.DelFlag = delNormal;
            UserInfoService.Update(userInfo);
            return Content("ok");
        }
        #endregion

        #region 设置用户角色
        public ActionResult SetUserRole(int id)
        {
            //拿到需要设置用户的信息
            var user = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = user;
            //前台需要的角色信息
            ViewBag.AllRoles = RoleInfoService.LoadEntities(r => r.DelFlag == delNormal).ToList();
            //待设置用户已经关联的角色Id信息
            ViewBag.ExistRolesId = user.RoleInfo.Select(r => r.Id).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult SetUserRole(int id, FormCollection collection)
        {
            int userId = id;
            //获得所有选中角色的id
            List<int> allSelectedRoleIds = new List<int>();
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    allSelectedRoleIds.Add(int.Parse(Request[key]));
                }
            }

            UserInfoService.SetUserRole(userId, allSelectedRoleIds);
            return Content("ok");
        }

        #endregion

        #region 设置用户特殊权限
        public ActionResult SetUserAction(int id)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == id).FirstOrDefault();

            //获得所有的权限信息
            ViewBag.AllActions = ActionInfoService.LoadEntities(a => a.DelFlag == delNormal).ToList();

            //获得当前用户的权限
            ViewBag.AllExistActions = user.R_UserInfo_ActionInfo.ToList();

            return View(user);
        }

        [HttpPost]
        public ActionResult SetUserAction(int id, int actionId, string isPass)
        {
            var temp = R_UserInfo_ActionInfoService.LoadEntities(r =>
                r.UserInfoId == id
                &&
                r.ActionInfoId == actionId).FirstOrDefault();
            if (isPass == "true" || isPass == "false")
            {
                bool pass = isPass == "true";

                if (temp != null)
                {
                    temp.IsPass = pass;
                    R_UserInfo_ActionInfoService.Update(temp);
                }
                else//不存在此权限。添加
                {
                    R_UserInfo_ActionInfo rUserInfoActionInfo = new R_UserInfo_ActionInfo();
                    rUserInfoActionInfo.UserInfoId = id;
                    rUserInfoActionInfo.IsPass = pass;
                    rUserInfoActionInfo.ActionInfoId = actionId;
                    R_UserInfo_ActionInfoService.Add(rUserInfoActionInfo);
                }

            }
            else//删除
            {
                if (temp != null)
                {
                    R_UserInfo_ActionInfoService.Delete(temp);
                }
            }
            return Content("ok");
        }
        #endregion

        #region 修改密码
        public ActionResult UpdateUserPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateUserPwd(string oldPwd, string newPwd, string newPwd2)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == CurrentLoginUser.Id).FirstOrDefault();
            if (user.Pwd != oldPwd)
            {
                return Content("密码错误！");
            }

            user.Pwd = newPwd;
            UserInfoService.Update(user);
            return Content("ok");
        }
        #endregion

    }
}
