using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{
   /// <summary>
   /// Ajax 返回
   /// </summary>
   /// <typeparam name="TResult"></typeparam>
    [Serializable]
    public class AjaxResponse<TData>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

       /// <summary>
       /// 如果成功 返回的数据
       /// </summary>
        public TData Data { get; set; }

       /// <summary>
       /// 错误信息包装
       /// </summary>
        public ErrorInfo Error { get; set; }

      
     

        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="data">返回的Data</param>
        public AjaxResponse(TData data)
        {
            Data = data;
            Success = true;
        }

       /// <summary>
       /// 默认 Success 为 true
       /// </summary>
        public AjaxResponse()
        {
            Success = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="success">是否成功</param>
        public AjaxResponse(bool success)
        {
            Success = success;
        }

       /// <summary>
       /// 出错时 返回
       /// </summary>
       /// <param name="error"></param>
     
        public AjaxResponse(ErrorInfo error)
        {
            Error = error;
            
            Success = false;
        }
    }
}
