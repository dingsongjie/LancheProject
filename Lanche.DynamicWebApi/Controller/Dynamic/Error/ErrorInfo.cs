﻿using Lanche.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.ModelBinding;

namespace Lanche.DynamicWebApi.Controller.Dynamic
{
    public class ErrorInfo : ITransientDependency
    {
        /// <summary>
        /// 此处自定义信息（友好信息）
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 系统错误信息
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 没有权限执行这个请求
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }
        public bool IsModelValidateError { get; set; }
        public ModelStateDictionary ModelState { get; set; }
        public ErrorInfo()
        {
            UnAuthorizedRequest = false;
        }
    }
}
