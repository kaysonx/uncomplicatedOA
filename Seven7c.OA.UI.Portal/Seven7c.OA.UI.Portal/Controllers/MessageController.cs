using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class MessageController : BaseController
    {
        public IMesssageService MesssageService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        public IRoleInfoService RoleInfoService { get; set; }

        public short delNormal = (short)Model.Enum.DelFlagEnum.Normal;

        
        #region 发送消息
        public ActionResult SendMessage()
        {
            ViewData["Roles"] = RoleInfoService.LoadEntities(r => r.DelFlag == delNormal)
                .ToList().Select(r => new SelectListItem() { Selected = false, Text = r.RoleName, Value = r.Id.ToString() });
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendMessage(Messsage message)
        {
            
            message.IsRead = false;
            message.SendTime = DateTime.Now;
            message.UserInfoId = CurrentLoginUser.Id;
            MesssageService.Add(message);
            if (message.Id <= 0)
            {
                return Content("error");
            }
            return Content("ok");
        }

        public ActionResult LoadUsers(int id)
        {
            var role = RoleInfoService.LoadEntities(r => r.Id == id).FirstOrDefault();


            if (role == null)
            {
                return Content("null");

            }
            var users = role.UserInfo;
            if (users == null)
            {
                return Content("null");
            }
            var data = from u in users
                       where u.DelFlag == delNormal
                       select new { u.Id, u.UName };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpLoadFile()
        {
            Response.ContentType = "text/plain";
            HttpPostedFileBase file = Request.Files["Filedata"];//接收文件.
            string fileName = Path.GetFileName(file.FileName);//获取文件名.
            string fileExt = Path.GetExtension(fileName);

            string dir = "/UpLoad/MessageFiles/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
            Directory.CreateDirectory(Path.GetDirectoryName(Server.MapPath(dir)));//创建文件夹
            string fullDir = dir + Common.MD5Helper.GetStreamMD5(file.InputStream) + fileExt;

            file.SaveAs(Server.MapPath(fullDir));

            return Content(fullDir);
        } 
        #endregion


        #region 消息列表
        
        public ActionResult GetMessage()
        {
            return View();
        }

        public ActionResult SetMessageRead(int id)
        {
            var message = MesssageService.LoadEntities(m => m.Id == id && m.DelFlagGet == delNormal).FirstOrDefault();
            if (message != null)
            {
                message.IsRead = true;
                MesssageService.Update(message);
            }
            return Content("ok");
        } 

        public ActionResult Detail(int id)
        {
            var message = MesssageService.LoadEntities(m => m.Id == id).FirstOrDefault();
            if(message != null)
            {
                message.IsRead = true;
                MesssageService.Update(message);
                return View(message);
            }
            else
            {
                return Content("暂未....");
            }
        }

        public ActionResult Delete(string Ids,string flag)
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

            MesssageService.DeleteMessages(idList,flag);

            return Content("ok");
        }

        public ActionResult LoadMessages()
        {
            //page:1
            //rows:20
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;
            var user = CurrentLoginUser;

            

            var data = MesssageService.LoadPageEntities(pageSize, pageIndex, out total, m => m.SendTo == user.Id && m.DelFlagGet == delNormal, false, m => m.SendTime)
                .Select(m => new { m.Id, m.SendTime, m.Title, m.Content, m.FileUrl, m.IsRead, m.UserInfoId, m.UserInfo.UName });

            var result = new { total = total, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region 管理发送消息记录
        public ActionResult ManageMessage()
        {
            return View();
        }
        public ActionResult LoadSendMessages()
        {
            //page:1
            //rows:20
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;
            var user = CurrentLoginUser;



            var data = MesssageService.LoadPageEntities(pageSize, pageIndex, out total, m => m.UserInfoId == user.Id && m.DelFlagSend == delNormal, false, m => m.SendTime)
                .Select(m => new { m.Id, m.SendTime, m.Title, m.Content, m.FileUrl, m.IsRead, m.UserInfo.UName, m.SendTo });

            List<Object> newData = new List<object>();
            foreach (var m in data)
            {
                var uName = GetUserName(m.SendTo);
                newData.Add(new { m.Id, m.SendTime, m.Title, m.Content, m.FileUrl, m.IsRead, m.UName, uName });
            }
            var result = new { total = total, rows = newData };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string GetUserName(int? uId)
        {
            var user = UserInfoService.LoadEntities(u => u.Id == uId && u.DelFlag == delNormal).FirstOrDefault();
            if (user != null)
            {
                return user.UName;
            }
            else
            {
                return "无..";
            }
        } 
        #endregion


        public ActionResult LoadNewMessages()
        {
            var user = CurrentLoginUser;

            var data = MesssageService.LoadEntities(m => m.SendTo == user.Id && m.DelFlagGet == delNormal && m.IsRead == false).OrderByDescending(m => m.SendTime).FirstOrDefault();

            
            if(data != null)
            {
                var result = new {data.Id,data.SendTime,data.Title,data.Content,data.FileUrl,data.UserInfo.UName};
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("null");
            }
            
        }
    }
}
