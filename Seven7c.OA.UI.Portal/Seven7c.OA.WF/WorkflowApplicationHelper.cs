using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seven7c.OA.WF
{
    public class WorkflowApplicationHelper
    {
        //工作流持久化数据库。
        private static readonly string Strsql;

        static WorkflowApplicationHelper()
        {
            Strsql = ConfigurationManager.AppSettings["wfSql"];
        }

        public static WorkflowApplication CreateWorkflowApp(Activity activity,Dictionary<string,object> dicParam)
        {
            WorkflowApplication wfApp = new WorkflowApplication(activity, dicParam);

            //注册常用工作流事件。
            wfApp.Idle += a =>
                {
                    Console.WriteLine("----------------工作流停下了--------------");
                };

            //工作流持久化。
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e)
            {
                 Console.WriteLine("工作卸载进行持久化,书签创建的时候就会执行序列化到数据库里面去。");
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a=>{
                Console.WriteLine("------------------工作流被卸载-----------");
            };

            wfApp.OnUnhandledException += a=>
                {
                    Console.WriteLine("出现了未处理的异常..............");
                    return UnhandledExceptionAction.Terminate;//终止工作流。
                };

            wfApp.Aborted += a=>
                {
                    Console.WriteLine("Aborted");
                };

            //创建持久化工作流实例的sqlStore对象。
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(Strsql);
            wfApp.InstanceStore = store;


            wfApp.Run();

            return wfApp;
        }

        public static WorkflowApplication ResumeBookMark(Activity activity, Guid instanceId, string bookmarkName, BaseResumeBookMarkValue value)
        {
            WorkflowApplication wfApp = new WorkflowApplication(activity);
                        //注册常用工作流事件。
            wfApp.Idle += a =>
                {
                    Console.WriteLine("----------------工作流停下了--------------");
                };

            //工作流持久化。
            wfApp.PersistableIdle = delegate(WorkflowApplicationIdleEventArgs e)
            {
                Console.WriteLine("工作卸载进行持久化,书签创建的时候就会执行序列化到数据库里面去。");
                return PersistableIdleAction.Unload;
            };

            wfApp.Unloaded += a=>{
                Console.WriteLine("------------------工作流被卸载-----------");
            };

            wfApp.OnUnhandledException += a=>
                {
                    Console.WriteLine("出现了未处理的异常..............");
                    return UnhandledExceptionAction.Terminate;//终止工作流。
                };

            wfApp.Aborted += a=>
                {
                    Console.WriteLine("Aborted");
                };

            //创建持久化工作流实例的sqlStore对象。
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(Strsql);
            wfApp.InstanceStore = store;

            //从数据库中加载当前工作流实例。
            wfApp.Load(instanceId);

            wfApp.ResumeBookmark(bookmarkName,value);

            return wfApp;
        }
    }
}
