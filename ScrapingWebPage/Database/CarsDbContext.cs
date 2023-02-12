using Microsoft.EntityFrameworkCore;
using ScrapingWebPage.Models;

namespace ScrapingWebPage.Database
{
    public class CarsDbContext : DbContext
    {
        public DbSet<CarPageModel> CarModels { get; set; }
        public DbSet<CarConfigurationModel> CarConfigurations { get; set; }
        public DbSet<CarConfigurationSpecModel> CarConfigurationSpecs { get; set; }
        public DbSet<GroupDetailsModel> GroupsDetails { get; set; }
        public DbSet<SubgroupDetailsModel> SubgroupsDetails { get; set; }
        public DbSet<DetailModel> Details { get; set; }

        public CarsDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer("Server=localhost;Database=CarsDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
