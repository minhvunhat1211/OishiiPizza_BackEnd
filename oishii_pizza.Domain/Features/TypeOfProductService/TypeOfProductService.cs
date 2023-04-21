using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.TypeOfProductModels;
using oishii_pizza.Domain.Models.UserModels;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.TypeOfProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.TypeOfProductService
{
    public class TypeOfProductService : ITypeOfProductService
    {
        private readonly ITypeOfProductRepository _typeOfProductRepository;
        public TypeOfProductService(ITypeOfProductRepository typeOfProductRepository)
        {
             _typeOfProductRepository = typeOfProductRepository;
        }
        public async Task<ApiResult<TypeOfProductDTO>> CreateAsync(TypeOfProductCreateRequest createRequest)
        {
            try
            {
                var newTypeOfProduct = new TypeOfProduct()
                {
                    Name = createRequest.Name,
                    CreateAt = DateTime.Now,
                    Status = 1
                };
                var data = new TypeOfProductDTO()
                {
                    Id= newTypeOfProduct.Id,
                    Name= newTypeOfProduct.Name,
                    Status= newTypeOfProduct.Status,
                    CreateAt= newTypeOfProduct.CreateAt,
                    UpdateAt= newTypeOfProduct.UpdateAt,
                    DeleteAt= newTypeOfProduct.DeleteAt,
                };
                await _typeOfProductRepository.CreateAsync(newTypeOfProduct);
                return new ApiSuccessResult<TypeOfProductDTO>(data);
            }
            catch (Exception)
            {

                return new ApiErrorResult<TypeOfProductDTO>("Loi");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var findTypeOfProductById = await _typeOfProductRepository.GetById(id);
                if (findTypeOfProductById.Status != 1)
                    return new ApiErrorResult<bool> { Message = "Tai khoan da duoc xoa" };
                findTypeOfProductById.Status = 2;
                await _typeOfProductRepository.UpdateAsync(findTypeOfProductById);
                return new ApiSuccessResult<bool> { Message = "Xoa thanh cong" };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<bool>> EditAsync(int id, TypeOfProductEditRequest request)
        {
            try
            {
                var findTypeOfProductById = await _typeOfProductRepository.GetById(id);
                findTypeOfProductById.Name = request.Name;
                findTypeOfProductById.UpdateAt = DateTime.Now;
                await _typeOfProductRepository.UpdateAsync(findTypeOfProductById);
                return new ApiSuccessResult<bool> { Message = "Thanh cong" };
            }
            catch (Exception)
            {

                return new ApiErrorResult<bool> { Message = "That bai" };
            }
        }

        public async Task<ApiResult<PagedResult<TypeOfProductDTO>>> GetAll(int? pageSize, int? pageIndex, string search)
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
                var totalRow = await _typeOfProductRepository.CountAsync();
                var query = await _typeOfProductRepository.GetAll(pageSize, pageIndex);
                if (!string.IsNullOrEmpty(search))
                {
                    Expression<Func<TypeOfProduct, bool>> expression = x => x.Name.Contains(search);
                    query = await _typeOfProductRepository.GetAll(pageSize, pageIndex, expression);
                    totalRow = await _typeOfProductRepository.CountAsync(expression);
                }
                //Paging
                var data = query
                    .Select(x => new TypeOfProductDTO()
                    {
                        Name = x.Name,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        DeleteAt = x.DeleteAt,
                        Id = x.Id,
                        Status = x.Status,
                    }).OrderByDescending(x => x.CreateAt).ToList();
                var pagedResult = new PagedResult<TypeOfProductDTO>()
                {
                    TotalRecord = totalRow,
                    PageSize = pageSize.Value,
                    PageIndex = pageIndex.Value,
                    Items = data
                };
                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<TypeOfProductDTO>>("Khong co data");
                }
                return new ApiSuccessResult<PagedResult<TypeOfProductDTO>>(pagedResult);
            }
            catch (Exception)
            {

                return new ApiErrorResult<PagedResult<TypeOfProductDTO>>("Loi");
            }
        }

        public async Task<ApiResult<TypeOfProductDTO>> GetByIdAsync(int id)
        {
            try
            {
                var findTypeOfProductById = await _typeOfProductRepository.GetById(id);
                if (findTypeOfProductById == null)
                {
                    return new ApiErrorResult<TypeOfProductDTO>("Khong ton tai loai san pham");
                }
                var data = new TypeOfProductDTO()
                {
                    Id = findTypeOfProductById.Id,
                    Name = findTypeOfProductById.Name,
                    CreateAt = findTypeOfProductById.CreateAt,
                    UpdateAt = findTypeOfProductById.UpdateAt,
                    DeleteAt = findTypeOfProductById.DeleteAt,
                    Status = findTypeOfProductById.Status
                };
                return new ApiSuccessResult<TypeOfProductDTO>(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<bool>> UnDeleteAsync(int id)
        {
            try
            {
                var findTypeOfProductById = await _typeOfProductRepository.GetById(id);
                if (findTypeOfProductById.Status != 2)
                    return new ApiErrorResult<bool> { Message = "Tai khoan da duoc khoi phuc" };
                findTypeOfProductById.Status = 1;
                await _typeOfProductRepository.UpdateAsync(findTypeOfProductById);
                return new ApiSuccessResult<bool> { Message = "Khoi phuc thanh cong" };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
