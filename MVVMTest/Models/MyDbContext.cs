using System.Data.Entity;
using MVVMTest.Models;

namespace MVVMTest.Models
{
    class MyDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<TblUser> Users { get; set; }
        public DbSet<TblPermission> Permissions { get; set; }
        public DbSet<TblRule> Rules { get; set; }
    }
}