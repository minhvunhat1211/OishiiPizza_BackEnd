using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.ProductRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProduct(int? pageSize, int? pageIndex);
        Task<List<Product>> GetAllProduct(int? pageSize, int? pageIndex, Expression<Func<Product, bool>> expression);
        Task<Product> GetByIdProduct(int? productId);
    }
}
