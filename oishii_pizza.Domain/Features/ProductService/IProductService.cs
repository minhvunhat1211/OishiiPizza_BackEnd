using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.ProductModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.ProductService
{
    public interface IProductService
    {
        public Task<ApiResult<ProductDTO>> CreateAsync(int TypeOfProductId, ProductCreateRequest createRequest);
        public Task<ApiResult<ProductDTO>> GetByIdAsync(int id);
        public Task<ApiResult<PagedResult<ProductDTO>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<PagedResult<ProductDTO>>> GetAllByTypeOfProductname(int? pageSize, int? pageIndex, string typeOfProductName);
    }
}
