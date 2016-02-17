using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{  
    /// <summary>
    /// 返回 json 
    /// </summary>
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
       /// <summary>
       ///  Success 为 true
       /// </summary>
        public AjaxResponse()
        {

        }

       /// <summary>
       /// 构造
       /// </summary>
       /// <param name="success">是否成功</param>
        public AjaxResponse(bool success)
            : base(success)
        {

        }

        /// <summary>
        /// 返回 data
        /// </summary>
        /// <param name="data"></param>
        public AjaxResponse(object data)
            : base(data)
        {

        }

       /// <summary>
       /// 返回错误信息
       /// </summary>
       /// <param name="error"></param>
       /// <param name="unAuthorizedRequest"></param>
        public AjaxResponse(ErrorInfo error)
            : base(error)
        {

        }
    }
}
