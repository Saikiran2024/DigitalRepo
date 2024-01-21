using DTribe.Core.Entities;
using DTribe.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DTribe.DB
{
    public static class DtribeDBConnectionStringProvider
    {
        private static IConfiguration? _configuration;

        public static void Initilize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("DigitaltribedbConnection");
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found in configuration");
            }
            return connectionString;
        }
    }
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GlobalCategories> TblGlobalCategories { get; set; }
        public DbSet<GlobalCategoryItems> TblGlobalCategoryItems { get; set; }
        public DbSet<Categories> TblCategories { get; set; }
        public DbSet<CategoryItem> TblCategoryItems { get; set; }
        public DbSet<UserInfo> TblUser { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(DtribeDBConnectionStringProvider.GetConnectionString());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define your entity configurations here
            // For example:
            // modelBuilder.Entity<YourEntity>().HasKey(e => e.Id);
            //modelBuilder.Entity<Users>().HasIndex(d => d.UserID).IsUnique();
            modelBuilder.Entity<Categories>().HasKey(u => u.CategoryID);
            modelBuilder.Entity<CategoryItem>().HasKey(u => u.CategoryItemID);
            modelBuilder.Entity<UserInfo>().HasKey(u => u.UserID);
        }
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                //string userId = "superadmin";
                //string password = "Digital@23";

                //if (userId == null || password == null)
                //{
                //    throw new InvalidOperationException("User ID or Password environment variables not found");
                //}
                ////string connectionString = "Server=(local);Database=DTRIBE;User Id=Naveen;Password=Admin123;";
                ////string connectionString = "Server=(DTRIBE);Database=Test;Integrated Security=True;";
                //string connectionString = $"Server=tcp:digitaltribedb.database.windows.net,1433;Initial Catalog=digitaltribe;Persist Security Info=False;User ID=superadmin;Password={password};";

                optionsBuilder.UseSqlServer(DtribeDBConnectionStringProvider.GetConnectionString());

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
