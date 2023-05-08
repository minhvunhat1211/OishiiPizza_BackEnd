using Microsoft.EntityFrameworkCore;
using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.DbContext;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly OishiiPizzaDbContext _oishiiPizzaDbContext;
        public OrderRepository(OishiiPizzaDbContext db) : base(db)
        {
            _oishiiPizzaDbContext = db;
        }

        public async Task<IEnumerable<Order>> GetAllOrder(int? pageSize, int? pageIndex)
        {
            var query = _oishiiPizzaDbContext.Orders.Include(detail => detail.OrderDetails).AsQueryable();
            var pageCount = query.Count();
            query = query.Skip((pageIndex.Value - 1) * pageSize.Value)
            .Take(pageSize.Value);
            return query.ToList();
        }

        public async Task<Order> GetByIdOrder(int? orderId)
        {
            var query = _oishiiPizzaDbContext.Orders.Include(x => x.OrderDetails).Where(id => id.Id == orderId).FirstOrDefault();
            return query;
        }
    }
}
