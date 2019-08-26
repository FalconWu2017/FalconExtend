
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Falcon.DI.Test
{
    [FalconDIRegister(typeof(MyClassWithDisable),Lifetime =ServiceLifetime.Transient)]
    public class MyClassWithDisable:IDisposable
    {
        public static int Count { get; set; } = 0;

        public MyClassWithDisable() => Count += 1;

        public void Dispose() {
            Count -= 1;
        }
    }
}
