using Seven7c.OA.IBLL;
using Seven7c.OA.Model;
using Seven7c.OA.WF;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seven7c.OA.UI.Portal.Controllers
{
    public class WFInstanceController : BaseController
    {
        //
        // GET: /WFInstance/
        public IWF_TempService WF_TempService { get; set; }
        public IWF_InstanceService WF_InstanceService { get; set; }
        public IWF_StepService WF_StepService { get; set; }
        public IUserInfoService UserInfoService { get; set; }
        short delNormal = (short)Model.Enum.DelFlagEnum.Normal;
        //显示所有可发起的流程。
        public ActionResult Index()
        {
            ViewData.Model = WF_TempService.LoadEntities(u => u.DelFlag == delNormal).ToList();

            return View();
        }
        //创建一个流程实例。
        #region 发起一个流程
        public ActionResult Add(int tempId)
        {
            ViewData["toUsers"] =
                UserInfoService.LoadEntities(u => u.DelFlag == delNormal).ToList()
                .Select(u => new SelectListItem() { Selected = false, Text = u.UName, Value = u.Id.ToString() });
            ViewBag.Temp = WF_TempService.LoadEntities(t => t.Id == tempId).FirstOrDefault();
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(int tempId, WF_Instance instance, int toUsers)
        {
            var currentUserId = CurrentLoginUser.Id;

            //1.在工作流实例表中添加一条数据。
            //2.启动工作流。
            //3.在工作流步骤表中添加两条步奏：当前已经处理的步骤与下一待处理的步奏。

            instance.DelFlag = delNormal;
            instance.EndTime = DateTime.Now;
            instance.SubTime = DateTime.Now;

            instance.SubBy = currentUserId;
            instance.Status = (short)Model.Enum.WFInstanceStatusEnum.Running;

            instance.WF_TempId = tempId;
            instance.WFApplicationId = Guid.Empty;
            WF_InstanceService.Add(instance);

            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(new FinacleActivity(),
                new Dictionary<string, object>() { { "TempBookMarkName", "项目经理审批" } });

            instance.WFApplicationId = wfApp.Id;
            WF_InstanceService.Update(instance);

            WF_Step step = new WF_Step();
            step.CheckTime = DateTime.Now;
            step.Comment = "提交审批表单";
            step.DelFlag = delNormal;
            step.IsEndStep = false;
            step.IsStartStep = true;
            step.ProcessBy = currentUserId;
            step.Remark = "提交申请";
            step.Result = "通过呗";
            step.Status = (short)Model.Enum.WFStepStatucEnum.End;
            step.DelFlag = (short)Model.Enum.DelFlagEnum.Normal;
            step.SubTime = DateTime.Now;
            step.StepName = "项目经理审批";
            step.WF_InstanceId = instance.Id;

            WF_StepService.Add(step);

            WF_Step nextStep = new WF_Step();
            nextStep.WF_InstanceId = instance.Id;
            nextStep.CheckTime = DateTime.Now;
            nextStep.Comment = string.Empty;
            nextStep.DelFlag = delNormal;
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = toUsers;
            nextStep.Remark = string.Empty;
            nextStep.Result = string.Empty;
            nextStep.Status = (short)Model.Enum.WFStepStatucEnum.Running;
            nextStep.StepName = string.Empty;
            nextStep.SubTime = DateTime.Now;
            WF_StepService.Add(nextStep);


            return RedirectToAction("ShowMyCheck");
        } 
        #endregion


        #region 我申请的流程

        public ActionResult ShowMyCheck()
        {
            var data = WF_InstanceService.LoadEntities(i => i.SubBy == CurrentLoginUser.Id).ToList();
            return View(data);
        } 
        #endregion



        #region 我待批的流程
        public ActionResult ShowMyUnCheck()
        {
            var runEnum = (short)Model.Enum.WFStepStatucEnum.Running;

            var steps = WF_StepService.LoadEntities(s => s.Status == runEnum
                && s.ProcessBy == CurrentLoginUser.Id).ToList();
            var instanceIds = (from s in steps
                               select s.WF_InstanceId).Distinct();

            var data = WF_InstanceService.LoadEntities(i =>
                instanceIds.Contains(i.Id) && i.Status == runEnum).ToList();

            return View(data);
        } 
        #endregion

        #region 我批完的流程
        public ActionResult ShowMyChecked()
        {
            var endEnum = (short)Model.Enum.WFStepStatucEnum.End;
            var steps = WF_StepService.LoadEntities(s => s.Status == endEnum
                && s.ProcessBy == CurrentLoginUser.Id).ToList();

            var instanceIds = (from s in steps
                               select s.WF_InstanceId).Distinct();
            var data = WF_InstanceService.LoadEntities(i => i.Status == endEnum
                && instanceIds.Contains(i.Id)).ToList();

            return View(data);
        } 
        #endregion

        #region 审批
        public ActionResult DoCheck(int id)
        {
            var instance = WF_InstanceService.LoadEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.Instance = instance;

            ViewBag.Steps = instance.WF_Step.ToList();

            ViewData["toUsers"] = UserInfoService.LoadEntities(u => u.DelFlag == delNormal).ToList()
               .Select(u => new SelectListItem() { Selected = false, Text = u.UName, Value = u.Id.ToString() });
            return View();
        }

        [HttpPost]
        public ActionResult DoCheck(int stepid, bool isPass, string Comment, int toUsers)
        {
            //更新当前步骤的信息。
            var step = WF_StepService.LoadEntities(s => s.Id == stepid).FirstOrDefault();
            step.Result = isPass ? "通过" : "不通过";
            step.Status = (short)Model.Enum.WFStepStatucEnum.End;
            step.CheckTime = DateTime.Now;
            step.Remark = string.Empty;
            step.Comment = Comment;
            WF_StepService.Update(step);

            //保存下一步骤。
            WF_Step nextStep = new WF_Step();
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = toUsers;
            nextStep.SubTime = DateTime.Now;
            nextStep.CheckTime = DateTime.Now;
            nextStep.Remark = string.Empty;
            nextStep.Result = string.Empty;
            nextStep.Status = (short)Model.Enum.WFStepStatucEnum.Running;
            nextStep.DelFlag = (short)Model.Enum.DelFlagEnum.Normal;
            nextStep.StepName = string.Empty;
            nextStep.WF_InstanceId = step.WF_InstanceId;
            WF_StepService.Add(nextStep);

            //让工作流继续执行。
            BaseResumeBookMarkValue value = new BaseResumeBookMarkValue();
            value.BookMarkName = "";
            value.Value = isPass ? 1 : -1;
            value.InstanceId = step.WF_Instance.WFApplicationId;

            WorkflowApplicationHelper.ResumeBookMark(
                new FinacleActivity(), step.WF_Instance.WFApplicationId,
                step.StepName, value);
            return RedirectToAction("ShowMyUnCheck");
        } 
        #endregion

        public string GetSubByName(int subById)
        {
            var user =  UserInfoService.LoadEntities(u => u.Id == subById).FirstOrDefault();
            if(user == null)
            {
                return string.Empty;
            }
            return user.UName;
        }

        public  ActionResult Detail(int id)
        {
            var instance = WF_InstanceService.LoadEntities(u => u.Id == id).FirstOrDefault();
            ViewBag.Instance = instance;

            var steps = instance.WF_Step;
            return View(steps);
        }
    }
}
