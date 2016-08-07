using Seven7c.OA.Common;
using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public IUserInfoService UserInfoService { get; set; }
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        short menuType = (short)Model.Enum.ActionInfoTypeEnun.MenuAction;
        public ActionResult Index()
        {
            var user = UserInfoService.LoadEntities(u => u.Id == CurrentLoginUser.Id).FirstOrDefault();

            
            //1.通过用户的角色拿到菜单权限.
            var temp = (from r in user.RoleInfo
                        from a in r.ActionInfo
                        where a.ActionInfoType == menuType
                        where a.DelFlag == delNormal
                        select a).ToList();
            //2.通过特殊中间表拿到权限.
            var specialTemp = (from r in user.R_UserInfo_ActionInfo
                               where r.IsPass == true
                               select r.ActionInfo).ToList();
            //合并权限
            temp.AddRange(specialTemp);
            //移除拒绝的权限
            var userNoPass = (from r in user.R_UserInfo_ActionInfo
                              where r.IsPass = false
                              select r.ActionInfo.Id).ToList();

            var result = (from t in temp
                          where !userNoPass.Contains(t.Id)
                          where t.ActionInfoType == menuType
                          select t).ToList();
            //前台数据格式：{ icon: '/Content/images/3DSMAX.png', title: '用户管理', url: '/UserInfo/Index' },
            var data = (from r in result
                        select new { icon = r.IconUrl, title = r.ActionName, url = r.Url }).ToList();

            System.Web.Script.Serialization.JavaScriptSerializer javascriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ViewBag.MenuData = javascriptSerializer.Serialize(data);


            return View();
        }

        public ActionResult Loginout()
        {
            string guid = Request["mysessionId"];
            if (!string.IsNullOrEmpty(guid))
            {
                //从分布式缓存拿出来的对象不能进行延迟加载
                var loginUserInfo = CacheHelper.GetCache(guid) as UserInfo;
                if (loginUserInfo != null)
                {
                    Response.Cookies["mysessionId"].Expires = DateTime.Now.AddMinutes(-1);
                    CurrentLoginUser = null;
                    CacheHelper.WriteCache(guid, loginUserInfo, DateTime.Now.AddMinutes(-2));
                }
                
            }
            return Content("ok");
        }
        #region Test Cache
        public ActionResult WriteCache()
        {
            CacheHelper.WriteCache("hhh", "hahaha");
            return Content("ok");
        } 
        #endregion
    }
}
