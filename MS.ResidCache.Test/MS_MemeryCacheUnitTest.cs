using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MS.ResidCache.Test
{
    [TestClass]
    public class MS_MemeryCacheUnitTest
    {
        [TestMethod]
        public void IDistributedCacheTestMethod() {

            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions {

            });
            var key = "mcut_a";
            cache.Set(key,key);
            var r = cache.Get(key);
            Assert.AreEqual(r,key);
            cache.Remove(key);
            r = cache.Get(key);
            Assert.IsNull(r);

            if(cache is MemoryCache dis) {
                dis.Dispose();
            }
        }
    }
}
