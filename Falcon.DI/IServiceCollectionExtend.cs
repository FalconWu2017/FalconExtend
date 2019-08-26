using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using msdi = Microsoft.Extensions.DependencyInjection;

namespace Falcon.DI
{
    /// <summary>
    /// 服务集合方法扩展
    /// </summary>
    public static class IServiceCollectionExtend
    {
        /// <summary>
        /// 实现自动DI注册
        /// </summary>
        /// <param name="services">服务集合</param>
        public static IServiceCollection UseFalconDI(this IServiceCollection services,params Assembly[] assemblies) {
            if(assemblies == null || assemblies.Length == 0) {
                return services.UseFalconDI(AppDomain.CurrentDomain.GetAssemblies());
            }
            foreach(Assembly ass in assemblies) {
                foreach(Type type in ass.GetTypes()) {
                    var ra = type.GetCustomAttribute<FalconDIRegisterAttribute>(true);
                    if(ra != null) {
                        //如果未提供服务类型，注册到所有实现的接口
                        ra.ServiceTypes = ra.ServiceTypes ?? type.GetInterfaces();
                        //如果未实现任何接口，注册到类型本身
                        if(ra.ServiceTypes == null || ra.ServiceTypes.Length == 0) {
                            ra.ServiceTypes = new Type[] { type };
                        }
                        foreach(var ser in ra.ServiceTypes) {
                            services.Add(new ServiceDescriptor(ser,type,convertLifetime(ra.Lifetime)));
                        }
                    }
                }
            }
            return services;
        }
        /// <summary>
        /// 转换生存期枚举
        /// </summary>
        /// <param name="lt">生存期</param>
        /// <returns></returns>
        private static msdi.ServiceLifetime convertLifetime(ServiceLifetime lt) {
            return (msdi.ServiceLifetime)(int)lt;
        }
    }
}
