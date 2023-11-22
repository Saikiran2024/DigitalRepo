using DTribe.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DTribe.DB;
using Microsoft.EntityFrameworkCore.Design;

namespace DTribe.DB
{ 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categories> TblCategories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string userId = "superadmin";
            string password = "Digital@23";

            if (userId == null || password == null)
            {
                throw new InvalidOperationException("User ID or Password environment variables not found");
            }

            string connectionString = $"Server=tcp:digitaltribedb.database.windows.net,1433;Initial Catalog=digitaltribe;Persist Security Info=False;User ID=superadmin;Password={password};";

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define your entity configurations here
            // For example:
            // modelBuilder.Entity<YourEntity>().HasKey(e => e.Id);
            //modelBuilder.Entity<Users>().HasIndex(d => d.UserID).IsUnique();
            modelBuilder.Entity<Categories>().HasKey(u => u.CategoryID);
        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                string userId = "superadmin";
                string password = "Digital@23";

                if (userId == null || password == null)
                {
                    throw new InvalidOperationException("User ID or Password environment variables not found");
                }
                //string connectionString = "Server=(local);Database=DTRIBE;User Id=Naveen;Password=Admin123;";
                //string connectionString = "Server=(DTRIBE);Database=Test;Integrated Security=True;";
                string connectionString = $"Server=tcp:digitaltribedb.database.windows.net,1433;Initial Catalog=digitaltribe;Persist Security Info=False;User ID=superadmin;Password={password};";

                optionsBuilder.UseSqlServer(connectionString);

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
