﻿using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.OrderRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetByIdOrder(int? orderId);
        Task<IEnumerable<Order>> GetAllOrder(int? pageSize, int? pageIndex);
    }
}
