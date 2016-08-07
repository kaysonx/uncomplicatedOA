using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Seven7c.OA.WF
{
    //书签使用方法。
    //1.更改继承类
    public sealed class WaitInputDataActivity<T> : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InOutArgument<string> BookMarkName { get; set; }
        public OutArgument<T> OutResult { get; set; }

        //2.重写属性。
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        //3.修改传入参数。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 BookMarkName 输入输出参数的运行时值
            string text = context.GetValue(this.BookMarkName);
            context.CreateBookmark(text, new BookmarkCallback(MyCallBackFun));
        }

        private void MyCallBackFun(NativeActivityContext context, Bookmark bookmark, object value)
        {
            var data = value as BaseResumeBookMarkValue;
            if(data != null)
            {
                context.SetValue(OutResult, (T)data.Value);
                context.SetValue(BookMarkName, data.BookMarkName);
            }
        }
    }
}
