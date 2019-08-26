using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace MS.ResidCache.Test
{
    [TestClass]
    public class MicrosoftRedisCacheUnitTest
    {
        [TestMethod]
        public void IDistributedCacheTestMethod1() {
            IDistributedCache cache = new RedisCache(new RedisCacheOptions {
                Configuration = "127.0.0.1:7001,password=123654",
                InstanceName = "mcut",
            });
            var key = "mcut_a";
            cache.SetString(key,key);
            var r = cache.GetString(key);
            Assert.AreEqual(r,key);
            cache.Remove(key);
            r = cache.GetString(key);
            Assert.IsNull(r);

            if(cache is RedisCache dis) {
                dis.Dispose();
            }
        }
    }
}
