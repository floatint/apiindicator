using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace apiindserver.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Project> Projects { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<Version> Versions { set; get; }
        public DbSet<Criteria> Criterias { set; get; }
        public DbSet<LogRecord> LogRecords { set; get; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
