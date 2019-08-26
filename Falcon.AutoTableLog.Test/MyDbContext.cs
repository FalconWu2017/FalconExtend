using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Falcon.AutoTableLog.Test
{
    public static class MyDbContextFactory
    {
        public static MyDbContext Create() {
            var option = new DbContextOptionsBuilder<MyDbContext>().UseInMemoryDatabase("MemDb").Options;
            return new MyDbContext(option);
        }
    }

    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; }
    }

    [Table("Person")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
