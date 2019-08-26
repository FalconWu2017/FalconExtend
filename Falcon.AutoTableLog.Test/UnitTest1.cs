using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Falcon.AutoTableLog.Test
{
    [TestClass]
    public class DbContextAutoSaveLogTest
    {
        [TestMethod]
        public void GetDbContextEventTest() {
            using(var db=new MyDbContext()) {
                var log = new DbContextAutoSaveLog<MyDbContext>(db);
                log.GetDbContextEvent();


                var newP = new Person { Name = "p1"};
                db.Add(newP);
                db.SaveChangesAsync().Wait();
            }
        }
    }
}
