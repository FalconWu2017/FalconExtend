using Microsoft.VisualStudio.TestTools.UnitTesting;
using Falcon.ModelSP;
using System.Linq;
using Falcon.ModelSP.Test.Db;

namespace Falcon.ModelSP.Test
{
    [TestClass]
    public class ModelSPTest
    {
        [TestMethod]
        public void getParamsTest() {
            var model = new ParmModel { Id = 1,Name = "n1",AAAge = 10 };

            var data = DataExtend.getParams(model);
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count() == typeof(ParmModel).GetProperties().Length - 1);

            Assert.IsTrue(data.Any(m => m.ParameterName == "@Id" && m.Value.Equals(1)));
            Assert.IsTrue(data.Any(m => m.ParameterName == "@Name" && m.Value.Equals("n1")));
            Assert.IsTrue(data.Any(m => m.ParameterName == "@age" && m.Value.Equals(10)));
            Assert.IsTrue(!data.Any(m => m.ParameterName == "@Address"));
        }

        [TestMethod]
        public void getProcuderNameTest() {
            var model = new ParmModel { };
            Assert.IsTrue(DataExtend.getProcuderName<ParmModel>() == "ParmModel");
            Assert.IsTrue(DataExtend.getProcuderName<ParmModel1>() == "sp123");
        }

        class ParmModel
        {
            public int Id { get; set; }

            public string Name { get; set; }

            [FalconSPPrarmName("age")]
            public int AAAge { get; set; }

            [FalconSPIgnore]
            public string Address { get; set; }
        }

        [FalconSPProcuderName("sp123")]
        class ParmModel1 { }

        [TestMethod]
        public void InMemoryDbTest() {
            //内存数据库无法使用关系模型，测试无法进行
            return;
            using(var db = TestDbContext.GetDbInMemory()) {
                var re = db.Pr_AddOne(new Pr_AddOne { A = 1 });
                Assert.IsTrue(re != null);
                Assert.IsTrue(re.Any());
                var first = re.First();
                Assert.IsTrue(first.Aa == 2);
            }
        }
    }
}
