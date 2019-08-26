using System;

namespace Falcon.DI
{
    /// <summary>
    /// 自动注册服务特性。如果要注册到特定服务可以指定，否则注册到所有实现的接口服务，如果未实现任何接口注册到类型本身。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class FalconDIRegisterAttribute:Attribute
    {
        /// <summary>
        /// 默认注册到所有实现的基础接口
        /// </summary>
        public FalconDIRegisterAttribute() { }
        /// <summary>
        /// 注册到提供的服务类型
        /// </summary>
        /// <param name="type">服务类型</param>
        public FalconDIRegisterAttribute(params Type[] type) => this.ServiceTypes = type;

        /// <summary>
        /// 注册的服务类型集合
        /// </summary>
        public Type[] ServiceTypes { get; set; } = null;
        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Scoped;
    }
}
