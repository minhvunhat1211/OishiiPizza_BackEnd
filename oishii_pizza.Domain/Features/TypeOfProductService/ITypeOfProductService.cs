using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.TypeOfProductModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.TypeOfProductService
{
    public interface ITypeOfProductService
    {
        public Task<ApiResult<TypeOfProductDTO>> CreateAsync(TypeOfProductCreateRequest createRequest);
        public Task<ApiResult<TypeOfProductDTO>> GetByIdAsync(int id);
        public Task<ApiResult<PagedResult<TypeOfProductDTO>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<bool>> EditAsync(int id, TypeOfProductEditRequest request);
        public Task<ApiResult<bool>> DeleteAsync(int id);
        public Task<ApiResult<bool>> UnDeleteAsync(int id);
    }
}
