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
        public DbSet<Criteria> Criterias { set; get; }
        public DbSet<LogRecord> LogRecords { set; get; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //helper tables

            modelBuilder.Entity<ProjectTester>()
                .HasKey(x => new { x.ProjectId, x.TesterId });
            //project.testers
            modelBuilder.Entity<ProjectTester>()
                .HasOne(x => x.Project)
                .WithMany(y => y.Testers)
                .HasForeignKey(x => x.ProjectId);
            //tester.projects
            modelBuilder.Entity<ProjectTester>()
                .HasOne(x => x.Tester)
                .WithMany(y => y.Projects)
                .HasForeignKey(x => x.TesterId);

            modelBuilder.Entity<UserRole>()
                .HasKey(x => new { x.UserId, x.RoleId });
            //user.roles
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.Roles)
                .HasForeignKey(x => x.UserId);
            //role.users
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
