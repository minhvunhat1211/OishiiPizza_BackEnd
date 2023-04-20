using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.DbContext
{
    public class OishiiPizzaDbContextFactory : IDesignTimeDbContextFactory<OishiiPizzaDbContext>
    {
        public OishiiPizzaDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("OiShiiPizza");


            var optionsBuilder = new DbContextOptionsBuilder<OishiiPizzaDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OishiiPizzaDbContext(optionsBuilder.Options);
        }
    }
}
