namespace Falcon.DI
{
    /// <summary>
    /// 服务生存期
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton = 0,
        /// <summary>
        /// 每个Scoped内单例
        /// </summary>
        Scoped = 1,
        /// <summary>
        /// 瞬时，每次返回不同的对象
        /// </summary>
        Transient = 2
    }
}
