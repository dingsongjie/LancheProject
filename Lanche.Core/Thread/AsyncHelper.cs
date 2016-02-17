using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.Core.Thread
{
    /// <summary>
    /// 异步方法帮助类
    /// </summary>
    public static class AsyncHelper
    {
       /// <summary>
       /// 判断是否为异步方法
       /// </summary>
       /// <param name="method"></param>
       /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

       /// <summary>
       /// 同步运行一个异步方法，有返回值
       /// </summary>
       /// <typeparam name="TResult"></typeparam>
       /// <param name="func"></param>
       /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return AsyncContext.Run(func);
        }

      /// <summary>
      /// 同步运行一个异步方法，无返回值
      /// </summary>
      /// <param name="action"></param>
        public static void RunSync(Func<Task> action)
        {
            AsyncContext.Run(action);
        }
    }
}
