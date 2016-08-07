using Seven7c.OA.Common;
using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo CurrentLoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //校验用户是否已经登录
            #region Session模式
            //if(filterContext.HttpContext.Session["LoginUser"]==null)
            //{
            //    filterContext.HttpContext.Response.Redirect("/Login/Index");

            //}
            //else
            //{
            //    CurrentLoginUser = (UserInfo)filterContext.HttpContext.Session["LoginUser"];
            //} 
            #endregion
            //分布式Memcached模式。
            string guid = Request["mysessionId"];
            if (!string.IsNullOrEmpty(guid))
            {
                //从分布式缓存拿出来的对象不能进行延迟加载
                var loginUserInfo = CacheHelper.GetCache(guid) as UserInfo;
                if (loginUserInfo != null)
                {
                    CurrentLoginUser = loginUserInfo;
                    //滑动，使Session再加20分钟。
                    CacheHelper.WriteCache(guid, loginUserInfo, DateTime.Now.AddMinutes(20));

                    //检验用户是否拥有此地址的访问权限。
                    ValidateUserAcesess(loginUserInfo);
                    return;
                }
            }

            filterContext.HttpContext.Response.Redirect("/Login/Index");
        }

        private void ValidateUserAcesess(UserInfo loginUserInfoTemp)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();

            var ActionInfoService = ctx.GetObject("ActionInfoService") as IActionInfoService;
            var UserInfoService = ctx.GetObject("UserInfoService") as IUserInfoService;

            var userInfo = UserInfoService.LoadEntities(u => u.Id == loginUserInfoTemp.Id).FirstOrDefault();

            if(userInfo.UName == "admin")
            {
                return;
            }

            string strPath = Request.Url.AbsolutePath.ToLower();
            var httpMethod = Request.HttpMethod.ToLower();

            var tempAction = ActionInfoService.LoadEntities(a => a.Url.ToLower() == strPath
                && a.HttpMethod.ToLower() == httpMethod
                ).FirstOrDefault();

            if(tempAction ==null)
            {
                //地址非法。
                Response.Redirect("/Error.html");
            }

            //1.检验用户的特殊权限表中是否允许了这个地址。
            var temp = (from r in userInfo.R_UserInfo_ActionInfo
                        where r.ActionInfoId == tempAction.Id
                        select r).FirstOrDefault();

            if(temp != null)
            {
                if(temp.IsPass)
                {
                    return;
                }
                else
                {
                    Response.Redirect("/Error.html");
                }
            }

            //2.检验用户对应角色关联的所有权限。
            var data = from r in userInfo.RoleInfo
                       from a in r.ActionInfo
                       select a;

            var result = (from a in data
                          where a.Id == tempAction.Id
                          select a.Id).FirstOrDefault();
            if(result <= 0)
            {
                Response.Redirect("/Error.html");
            }


        }
    }
}
