using Microsoft.VisualStudio.TestTools.UnitTesting;
using Falcon.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Falcon.DI.Packget.Test
{
    [TestClass]
    public class FalconDITest
    {
        [TestMethod]
        public void RunTest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            var pd = ser.BuildServiceProvider();
            IA obj = pd.GetService<IA>();
            Assert.IsNotNull(obj);
        }
    }

    public interface IA { }

    [FalconDIRegister]
    public class A:IA { }
}
