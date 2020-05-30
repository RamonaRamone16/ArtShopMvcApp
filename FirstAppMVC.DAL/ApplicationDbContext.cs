using FirstAppMVC.DAL.Entities;
using FirstAppMVC.DAL.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;

namespace FirstAppMVC.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public readonly IEntityConfigurationContainer _entityConfigurationContainer;
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IEntityConfigurationContainer entityConfigurationContainer) : base(options)
        {
            _entityConfigurationContainer = entityConfigurationContainer;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity(_entityConfigurationContainer.ProductConfiguration.ProvideConfigurationAction());
            builder.Entity(_entityConfigurationContainer.CategoryConfiguration.ProvideConfigurationAction());
            builder.Entity(_entityConfigurationContainer.BrandConfiguration.ProvideConfigurationAction());
            builder.Entity(_entityConfigurationContainer.OrderConfiguration.ProvideConfigurationAction());
        }
    }
}
