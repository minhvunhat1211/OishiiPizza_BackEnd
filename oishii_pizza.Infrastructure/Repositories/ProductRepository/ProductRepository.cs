using Microsoft.EntityFrameworkCore;
using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.DbContext;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.TypeOfProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex)
        {
            var query = _oishiiPizzaDbContext.Products.Include(img => img.ProductImgs).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }

        public Task<List<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression)
        {
            var query = _oishiiPizzaDbContext.Products.Include(img => img.ProductImgs).Where(expression).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToListAsync();
        }

        public async Task<Product> GetByIdProduct(int? productId)
        {
            var query = _oishiiPizzaDbContext.Products.Include(x => x.ProductImgs).Where(id => id.Id == productId).FirstOrDefault();
            return query;
        }
    }
}
