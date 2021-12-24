using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StudentPersonalAccount.DBContext
{
    public class UserContext : DbContext
    {
        public UserContext() => Database.EnsureCreated();

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserData> UserDatas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DBStudyPA.db");
        }
    }
}
