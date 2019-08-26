namespace Falcon.DI.Test
{
    [FalconDIRegister(Lifetime = ServiceLifetime.Singleton)]
    class SingletonTest
    {
        public int Val { get; set; } = 0;
    }
}
