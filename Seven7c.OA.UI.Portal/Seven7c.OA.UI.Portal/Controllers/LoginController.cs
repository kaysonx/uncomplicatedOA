using Seven7c.OA.Common;
using Seven7c.OA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        IUserInfoService UserInfoService { get; set; }
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string LoginCode, string LoginPwd, string vCode)
        {
            //1.检验验证码
            string strCode = Session["LoginCode"] == null ? string.Empty : Session["LoginCode"].ToString();
            Session["LoginCode"] = null;//验证码只用一次,防止暴力破解

            if(string.IsNullOrEmpty(vCode))
            {
                return Content("请输入验证码!");
            }
            if(strCode.ToLower() != vCode.ToLower())
            {
                return Content("验证码验证失败!");
            }
            //2.检验用户名密码
            var loginUser = UserInfoService.LoadEntities(u => u.UName == LoginCode && u.Pwd == LoginPwd && u.DelFlag == delNormal).FirstOrDefault();

            if(loginUser == null)
            {
                return Content("用户名或密码错误!");
            }
            //3.保存用户信息到Session
            //Session["LoginUser"] = loginUser;
            //原先：把用户登录状态放到Session里面去。因为Session缺陷(访问量大时，IIS重启导致进程内Session内丢失)，舍弃Session

            //用户memcached模拟Session
            Guid guid = Guid.NewGuid();
            //以guid为key，以登录用户为value放到mm里面去。
            Common.CacheHelper.WriteCache(guid.ToString(), loginUser, DateTime.Now.AddMinutes(20));
            //把guid写到cookie里面去
            Response.Cookies["mysessionId"].Value = guid.ToString();
            return Content("ok");
        }

        public ActionResult ShowVCode()
        {
            Common.ValidateCodeHelper codeHelper = new Common.ValidateCodeHelper();
            string strCode = codeHelper.CreateValidateCode(4);

            Session["LoginCode"] = strCode;
            byte[] data = codeHelper.CreateValidateGraphic(strCode);
            return File(data, "image/jpeg");
        }
    }
}
