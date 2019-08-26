using System;

namespace Falcon.DI
{
    /// <summary>
    /// 依赖注入实例化工厂
    /// </summary>
    public interface IFalconDIInstantFactory<T>
    {
        /// <summary>
        /// 通过给定容器实例化对象
        /// </summary>
        T Instance();
    }
}
