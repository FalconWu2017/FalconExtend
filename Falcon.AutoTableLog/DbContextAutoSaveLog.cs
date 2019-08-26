using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Falcon.AutoTableLog
{
    public class DbContextAutoSaveLog<TDbContext> where TDbContext : DbContext
    {
        public TDbContext Db { get; set; }

        public DbContextAutoSaveLog(TDbContext db) {
            this.Db = db;
        }

        public void GetDbContextEvent() {
            this.Db.ChangeTracker.Tracked += (object sender,EntityTrackedEventArgs e) => {
                var a = e.Entry.OriginalValues;
            };
            foreach(var en in this.Db.ChangeTracker.Entries()) {
            }
        }

    }
}
