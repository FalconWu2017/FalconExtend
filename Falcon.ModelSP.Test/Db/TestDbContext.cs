using Microsoft.EntityFrameworkCore;
using Falcon.ModelSP;
using System.Collections.Generic;

namespace Falcon.ModelSP.Test.Db
{
    class TestDbContext:DbContext
    {
        protected TestDbContext() { }
        protected TestDbContext(DbContextOptions option) : base(option) { }

        public static TestDbContext GetDbInMemory() {
            var option = new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase("TestDb").Options;
            var db = new TestDbContext(option);
            AddTestPr(db);
            return db;
        }

        public static void AddTestPr(TestDbContext db) {
            var sql = @"
                    DROP PROC Pr_AddOne
                    CREATE PROCEDURE Pr_AddOne
                        @a AS INT
                    AS
                    Set Nocount On  
	                    SELECT @a+1 aa;
                    GO
            ";
            //db.Database.ExecuteSqlCommandAsync(sql).Wait();
        }

        public  IEnumerable<Pr_AddOneResult> Pr_AddOne(Pr_AddOne data) {
            return this.RunProcuder<Pr_AddOne,Pr_AddOneResult>(data);
        }
    }

    public class Pr_AddOne
    {
        public int A { get; set; }
    }
    public class Pr_AddOneResult
    {
        public int Aa { get; set; }
    }
}
