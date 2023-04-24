using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.DbContext;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.TypeOfProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly OishiiPizzaDbContext _oishiiPizzaDbContext;
        public ProductRepository(OishiiPizzaDbContext db) : base(db)
        {
            _oishiiPizzaDbContext = db;
        }
    }
}
