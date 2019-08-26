using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Falcon.DI.Test
{
    [TestClass]
    public class UseFalconDITest
    {
        /// <summary>
        /// 一般获取服务测试
        /// </summary>
        [TestMethod]
        public void DITest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            using(var pd = ser.BuildServiceProvider()) {
                Assert.IsNotNull(pd.GetServices<IMyInterface>());
                System.Console.WriteLine(pd.GetServices<IMyInterface>().Count());
                Assert.IsTrue(pd.GetServices<IMyInterface>().Count() > 1);
                Assert.IsNotNull(pd.GetService<IMyInterface>());
            }
        }

        /// <summary>
        /// 测试释放资源
        /// </summary>
        [TestMethod]
        public void disableTest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            var pd = ser.BuildServiceProvider();

            var obj = pd.GetService<MyClassWithDisable>();
            Assert.IsNotNull(obj);
            Assert.AreEqual(1,MyClassWithDisable.Count);

            pd.Dispose();
            Assert.AreEqual(0,MyClassWithDisable.Count);

            using(pd = ser.BuildServiceProvider()) {
                obj = pd.GetService<MyClassWithDisable>();
                Assert.IsNotNull(obj);
                Assert.AreEqual(1,MyClassWithDisable.Count);
            }
            Assert.AreEqual(0,MyClassWithDisable.Count);
        }

        [TestMethod]
        [Description("测试不提供完整构造注入参数")]
        public void NotFullTest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            var pd = ser.BuildServiceProvider();

            var obj = pd.GetService<NotFullObj>();
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.F1);
            Assert.IsNull(obj.F2);
        }

        /// <summary>
        /// 测试服务工厂
        /// </summary>
        [TestMethod]
        public void IFalconDIFactoryTest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            using(var pd = ser.BuildServiceProvider()) {
                var obj = pd.GetService<IFalconDIInstantTypeFactory>();
                Assert.IsNotNull(obj);
                Assert.AreEqual(obj.Instance().Val,2);

                var obj2 = pd.GetService<IFalconDIInstantFactory<IFDIIT>>();
                Assert.IsNotNull(obj2);
                Assert.AreEqual(obj2.Instance().Val,2);

            }
        }

        /// <summary>
        /// 测试注册到自身
        /// </summary>
        [TestMethod]
        public void RegisterToSelf() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            using(var pd = ser.BuildServiceProvider()) {
                var obj = pd.GetService<RegisterToSelf>();
                Assert.IsNotNull(obj);
            }
        }

        /// <summary>
        /// 测试单例
        /// </summary>
        [TestMethod]
        public void SingletonTest() {
            IServiceCollection ser = new ServiceCollection();
            ser.UseFalconDI();
            //单例。两次获取为同一对象
            using(var pd = ser.BuildServiceProvider()) {
                var obj = pd.GetService<SingletonTest>();
                Assert.IsTrue(obj.Val == 0);
                obj.Val = 1;
                var obj2 = pd.GetService<SingletonTest>();
                Assert.IsTrue(obj.Val == 1);
            }
            //释放Provider后，释放单例对象
            using(var pd = ser.BuildServiceProvider()) {
                var obj = pd.GetService<SingletonTest>();
                Assert.IsTrue(obj.Val == 0);
                obj.Val = 1;
                var obj2 = pd.GetService<SingletonTest>();
                Assert.IsTrue(obj.Val == 1);
            }
        }
    }
}
