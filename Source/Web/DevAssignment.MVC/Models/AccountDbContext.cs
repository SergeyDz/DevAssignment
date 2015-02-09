using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DevAssignment.MVC.Models
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }
    }
}