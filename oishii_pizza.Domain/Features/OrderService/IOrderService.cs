using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.OrderModels;
using oishii_pizza.Domain.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.OrderService
{
    public interface IOrderService
    {
        public Task<ApiResult<OrderDTO>> CreateAsync(OrderCreateRequest createRequest);
        public Task<ApiResult<OrderDTO>> GetByIdAsync(int id);
        public Task<ApiResult<PagedResult<OrderDTO>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<bool>> EditAsync(int id, OrderEditRequest request);
        public Task<ApiResult<bool>> DeleteAsync(int id);
        public Task<ApiResult<bool>> UnDeleteAsync(int id);
    }
}
