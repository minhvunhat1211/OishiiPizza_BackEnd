using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.UserService
{
    public interface IUserService
    {
        public Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest loginRequest);
        public Task<ApiResult<UserDTO>> CreateAsync(CreateRequest createRequest);
        public Task<ApiResult<UserDTO>> GetByIdAsync(int id);
        public Task<ApiResult<PagedResult<UserDTO>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<bool>> EditAsync(int id, UserEditResquest request);
        public Task<ApiResult<bool>> DeleteAsync(int id);
        public Task<ApiResult<bool>> UnDeleteAsync(int id);
    }
}
