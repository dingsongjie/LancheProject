using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lanche.Core.Dependency
{
    /// <summary>
    /// 为注册提供两种选择 单例 和 多实例
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// 单例
        /// 
        /// </summary>
        Singleton,

        /// <summary>
        /// 多实例
        /// </summary>
        Transient
    }
}
