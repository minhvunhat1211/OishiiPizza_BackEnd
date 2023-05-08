using Microsoft.EntityFrameworkCore;
using oishii_pizza.Infrastructure.Configuration;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace oishii_pizza.Infrastructure.DbContext
{
    public class OishiiPizzaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public OishiiPizzaDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TypeOfProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImgConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TypeOfProduct> TypeOfProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
