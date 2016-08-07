using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Spring.Context;
using Spring.Context.Support;
using Seven7c.OA.IBLL;
using Seven7c.OA.BLL;

namespace Seven7c.OA.WF
{

    public sealed class SetStepActivity : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> StepName { get; set; }
        public InArgument<bool> IsEnd { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string stepName = context.GetValue(this.StepName);
            bool end = context.GetValue(this.IsEnd);

            Guid instantId = context.WorkflowInstanceId;

            IWF_InstanceService WF_InstanceService = new WF_InstanceService();
            IWF_StepService WF_StepService = new WF_StepService();
            //IApplicationContext ctx = ContextRegistry.GetContext();


            //var WF_InstanceService = ctx.GetObject("WF_InstanceService") as IWF_InstanceService;
            //var WF_StepService = ctx.GetObject("WF_StepService") as IWF_StepService;

            var instant = WF_InstanceService.LoadEntities(w=>w.WFApplicationId==instantId).FirstOrDefault();
            var step = instant.WF_Step.OrderBy(s=>s.Id).LastOrDefault();
            step.StepName = stepName;
            step.IsEndStep = end;

            if(end){
                step.Result = "审批结束";
                instant.Status = (short)Model.Enum.WFInstanceStatusEnum.End;
            }

            WF_InstanceService.Update(instant);
        }
    }
}
