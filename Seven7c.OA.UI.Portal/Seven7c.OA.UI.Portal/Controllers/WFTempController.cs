using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class WFTempController : BaseController
    {
        //
        // GET: /WFTemp/
        public IWF_TempService WF_TempService { get; set; }
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAllTemps()
        {
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int total = 0;


            var data = WF_TempService.LoadPageEntities(pageSize, pageIndex, out total,
                                                        u => u.DelFlag == delNormal,
                                                        true,
                                                        u => u.Id).ToList()
                                      .Select(u => new { u.Id, u.DelFlag, u.Remark, u.TempName });


            //{total:   rows:}
            var result = new { total = total, rows = data };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]//非法字符的处理。
        public ActionResult Add(WF_Temp temp)
        {
            temp.DelFlag = delNormal;
            temp.WFActivity = string.Empty;

            WF_TempService.Add(temp);
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

            WF_TempService.DeleteTemps(idList);

            return Content("ok");
        }

        public ActionResult Edit(int id)
        {
            ViewData.Model = WF_TempService.LoadEntities(u => u.Id == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(WF_Temp tempInfo)
        {

            WF_TempService.Update(tempInfo);
            return Content("ok");
        }
    }
}
