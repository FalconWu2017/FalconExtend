using Microsoft.Extensions.DependencyInjection;

namespace Falcon.ModelSP
{
    /// <summary>
    /// IServiceCollection扩展
    /// </summary>
    public static class IServiceCollectionExtend
    {
        /// <summary>
        /// 注册对数据库存储过程模型化的调用扩展
        /// </summary>
        /// <param name="services">注册服务集合</param>
        /// <returns></returns>
        public static IServiceCollection UseFalconSP(this IServiceCollection services) {
            return services.AddSingleton<IFalconSPRuner,FalconSPRuner>();
        }
    }
}
