using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.HashPass;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.UserModels;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ApiResult<UserDTO>> CreateAsync(CreateRequest createRequest)
        {

            var check = await _userRepository.GetByUserNameAsync(createRequest.Email);
            if (check != null)
            {
                return new ApiErrorResult<UserDTO>("Tai khoan da ton tai");
            }
            string hashedPass = createRequest.Password.Hash();
            var newUser = new User()
            {
                Email = createRequest.Email,
                Address = createRequest.Address,
                PhoneNumber = createRequest.PhoneNumber,
                CreateAt = DateTime.Now,
                Role = "USER",
                Password = hashedPass,
                Status = 1

            };
            var data = new UserDTO()
            {
                Email = newUser.Email,
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber,
                CreateAt = newUser.CreateAt.Value,
                Role = newUser.Role,
                Password = newUser.Password,
                Status = newUser.Status
            };
            await _userRepository.CreateAsync(newUser);
            return new ApiSuccessResult<UserDTO>(data);
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var findUserById = await _userRepository.GetById(id);
                if (findUserById.Status != 1)
                    return new ApiErrorResult<bool> { Message = "Tai khoan da duoc xoa" };
                findUserById.Status = 2;
                await _userRepository.UpdateAsync(findUserById);
                return new ApiSuccessResult<bool> { Message = "Xoa thanh cong"};
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<bool>> EditAsync(int id, UserEditResquest request)
        {
            try
            {
                var findUserById = await _userRepository.GetById(id);
                findUserById.PhoneNumber = request.PhoneNumber;
                findUserById.Address = request.Address;
                findUserById.UpdateAt = DateTime.Now;
                await _userRepository.UpdateAsync(findUserById);
                return new ApiSuccessResult<bool> { Message = "Thanh cong"};
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<PagedResult<UserDTO>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            try
            {
                if (pageSize != null)
                {
                    pageSize = pageSize.Value;
                }
                if (pageIndex != null)
                {
                    pageIndex = pageIndex.Value;
                }
                var totalRow = await _userRepository.CountAsync();
                var query = await _userRepository.GetAll(pageSize, pageIndex);
                if (!string.IsNullOrEmpty(search))
                {
                    Expression<Func<User, bool>> expression = x => x.Email.Contains(search);
                    query = await _userRepository.GetAll(pageSize, pageIndex, expression);
                    totalRow = await _userRepository.CountAsync(expression);
                }
                //Paging
                var data = query
                    .Select(x => new UserDTO()
                    {
                        Email = x.Email,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        DeleteAt = x.DeleteAt,
                        PhoneNumber = x.PhoneNumber,
                        Id = x.Id,
                        Address = x.Address,
                        Status = x.Status,
                        Role = x.Role,
                    }).OrderByDescending(x => x.CreateAt).ToList();
                var pagedResult = new PagedResult<UserDTO>()
                {
                    TotalRecord = totalRow,
                    PageSize = pageSize.Value,
                    PageIndex = pageIndex.Value,
                    Items = data
                };
                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<UserDTO>>("Khong co data");
                }
                return new ApiSuccessResult<PagedResult<UserDTO>>(pagedResult);
            }
            catch (Exception)
            {

                return new ApiErrorResult<PagedResult<UserDTO>>("Loi");
            }
        }

        public async Task<ApiResult<UserDTO>> GetByIdAsync(int id)
        {
            try
            {
                var findUserById = await _userRepository.GetById(id);
                if (findUserById == null)
                {
                    return new ApiErrorResult<UserDTO>("Khong ton tai user");
                }
                var user = new UserDTO()
                {
                    Id = findUserById.Id,
                    Email = findUserById.Email,
                    Address = findUserById.Address,
                    PhoneNumber = findUserById.PhoneNumber,
                    CreateAt = findUserById.CreateAt,
                    UpdateAt = findUserById.UpdateAt,
                    DeleteAt = findUserById.DeleteAt,
                    Role = findUserById.Role,
                    Status = findUserById.Status
                };
                return new ApiSuccessResult<UserDTO>(user);
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        public async Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest loginRequest)
        {
            /*var result = await _userRepository.GetByUserNameAsync(loginRequest.Email);*/
            Expression<Func<User, bool>> expression = x => x.Email == loginRequest.Email;
            var data = await _userRepository.GetByCondition(expression);
            var result = data.Select(x => new UserDTO()
            {
                Email = x.Email,
                CreateAt = x.CreateAt,
                UpdateAt = x.UpdateAt,
                DeleteAt = x.DeleteAt,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                Address = x.Address,
                Status = x.Status,
                Role = x.Role,
                Password = x.Password,
            }).OrderByDescending(x => x.CreateAt).FirstOrDefault();
            string hashedPass = loginRequest.Password.Hash();
            if (result == null)
            {
                return new ApiErrorResult<LoginResponse>("Sai tai khoan hoac mat khau");
            }
            if (result.Password != hashedPass)
            {
                return new ApiErrorResult<LoginResponse>("Sai tai khoan hoac mat khau");
            }
            var authClaim = new List<Claim>
            {
                new Claim("Id", result.Id.ToString()),
                new Claim("Email", result.Email),
                new Claim("Status", result.Status.ToString()),
                new Claim("Role", result.Role.ToString()),
                new Claim("ExpriseIn", "20"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaim,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
                );
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var TokenResult = new LoginResponse()
            {
                AccessToken = accessToken,
            };
            return new ApiSuccessResult<LoginResponse>(TokenResult);
        }

        public async Task<ApiResult<bool>> UnDeleteAsync(int id)
        {
            try
            {
                var findUserById = await _userRepository.GetById(id);
                if (findUserById.Status != 2)
                    return new ApiErrorResult<bool> { Message = "Tai khoan khong phai trang thai xoa" };
                findUserById.Status = 1;
                await _userRepository.UpdateAsync(findUserById);
                return new ApiSuccessResult<bool> { Message = "Khoi phuc thanh cong" };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
