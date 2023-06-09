﻿using Microsoft.AspNetCore.Http;
using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.FileStorage;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.ProductModels;
using oishii_pizza.Domain.Models.TypeOfProductModels;
using oishii_pizza.Domain.Models.UserModels;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileStorageService _storageService;
        public ProductService(IProductRepository productRepository, IFileStorageService storageService)
        {
            _productRepository = productRepository;
            _storageService = storageService;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        public async Task<ApiResult<ProductDTO>> CreateAsync(int TypeOfProductId, ProductCreateRequest createRequest)
        {
            try
            {
                var newProduct = new Product()
                {
                    TypeOfProductId = TypeOfProductId,
                    Description = createRequest.Description,
                    Name = createRequest.Name,
                    Price = createRequest.Price,
                    CreateAt = DateTime.Now,
                    Status = 1
                };
                var tempImg = new List<ProductImg>();
                if (createRequest.ProductImgs != null)
                {
                    foreach (var img in createRequest.ProductImgs)
                    {
                        var productImage = new ProductImg()
                        {
                            Caption = newProduct.Name,
                            ImagePath = await this.SaveFile(img),
                        };
                        tempImg.Add(productImage);
                    }
                    newProduct.ProductImgs = tempImg;
                }
                var data = new ProductDTO()
                {
                    Id = newProduct.Id,
                    TypeOfProductId = newProduct.TypeOfProductId,
                    Name = newProduct.Name,
                    Price = newProduct.Price,
                    Description = newProduct.Description,
                    Status = newProduct.Status,
                    CreateAt = newProduct.CreateAt,
                    UpdateAt = newProduct.UpdateAt,
                    DeleteAt = newProduct.DeleteAt,
                    ProductImgs = newProduct.ProductImgs
                };
                await _productRepository.CreateAsync(newProduct);
                return new ApiSuccessResult<ProductDTO>(data);
            }
            catch (Exception ex)
            {

                return new ApiErrorResult<ProductDTO>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var findProductById = await _productRepository.GetById(id);
                if (findProductById.Status != 1)
                    return new ApiErrorResult<bool> { Message = "Tai khoan da duoc xoa" };
                findProductById.Status = 2;
                await _productRepository.UpdateAsync(findProductById);
                return new ApiSuccessResult<bool> { Message = "Xoa thanh cong" };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResult<bool>> EditAsync(int id, ProductEditRequest request)
        {
            try
            {
                var findProductById = await _productRepository.GetById(id);
                findProductById.Name = request.Name;
                findProductById.Description = request.Description;
                findProductById.Price = request.Price;
                findProductById.TypeOfProductId = request.TypeOfProductId;
                findProductById.UpdateAt = DateTime.Now;
                var tempImg = new List<ProductImg>();
                if (request.ProductImgs != null)
                {
                    foreach (var img in request.ProductImgs)
                    {
                        var productImage = new ProductImg()
                        {
                            Caption = findProductById.Name,
                            ImagePath = await this.SaveFile(img),
                        };
                        tempImg.Add(productImage);
                    }
                    findProductById.ProductImgs = tempImg;
                }
                await _productRepository.UpdateAsync(findProductById);
                return new ApiSuccessResult<bool> { Message = "Thanh cong" };
            }
            catch (Exception)
            {

                return new ApiErrorResult<bool> { Message = "That bai" };
            }
        }

        public async Task<ApiResult<PagedResult<ProductDTO>>> GetAll(int? pageSize, int? pageIndex, string search)
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
                var totalRow = await _productRepository.CountAsync();
                var query = await _productRepository.GetAllProduct(pageSize, pageIndex);
                if (!string.IsNullOrEmpty(search))
                {
                    Expression<Func<Product, bool>> expression = x => x.Name.Contains(search);
                    query = await _productRepository.GetAllProduct(pageSize, pageIndex, expression);
                    totalRow = await _productRepository.CountAsync(expression);
                }
                //Paging
                var data = query
                    .Select(x => new ProductDTO()
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        TypeOfProductId = x.TypeOfProductId,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        DeleteAt = x.DeleteAt,
                        Id = x.Id,
                        Status = x.Status,
                        ProductImgs = x.ProductImgs,
                    }).OrderByDescending(x => x.CreateAt).ToList();
                var pagedResult = new PagedResult<ProductDTO>()
                {
                    TotalRecord = totalRow,
                    PageSize = pageSize.Value,
                    PageIndex = pageIndex.Value,
                    Items = data
                };
                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<ProductDTO>>("Khong co data");
                }
                return new ApiSuccessResult<PagedResult<ProductDTO>>(pagedResult);
            }
            catch (Exception)
            {

                return new ApiErrorResult<PagedResult<ProductDTO>>("Loi");
            }
        }

        public async Task<ApiResult<PagedResult<ProductDTO>>> GetAllByTypeOfProductname(int? pageSize, int? pageIndex, string typeOfProductName)
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
                Expression<Func<Product, bool>> expression = x => x.TypeOfProduct.Name == typeOfProductName;
                var query = await _productRepository.GetAllProduct(pageSize, pageIndex, expression);
                var totalRow = await _productRepository.CountAsync(expression);
                if (totalRow == 0)
                {
                    return new ApiErrorResult<PagedResult<ProductDTO>>("Khong co data");
                }
                //Paging
                var data = query
                    .Select(x => new ProductDTO()
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        TypeOfProductId = x.TypeOfProductId,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        DeleteAt = x.DeleteAt,
                        Id = x.Id,
                        Status = x.Status,
                        ProductImgs = x.ProductImgs,
                    }).OrderByDescending(x => x.CreateAt).ToList();
                var pagedResult = new PagedResult<ProductDTO>()
                {
                    TotalRecord = totalRow,
                    PageSize = pageSize.Value,
                    PageIndex = pageIndex.Value,
                    Items = data
                };
                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<ProductDTO>>("Khong co data");
                }
                return new ApiSuccessResult<PagedResult<ProductDTO>>(pagedResult);
            }
            catch (Exception)
            {

                return new ApiErrorResult<PagedResult<ProductDTO>>("Loi");
            }
        }

        public async Task<ApiResult<ProductDTO>> GetByIdAsync(int id)
        {
            try
            {
                var findProductById = await _productRepository.GetByIdProduct(id);
                if (findProductById == null)
                {
                    return new ApiErrorResult<ProductDTO>("Khong ton tai san pham");
                }
                var data = new ProductDTO()
                {
                    Id = findProductById.Id,
                    Name = findProductById.Name,
                    Description = findProductById.Description,
                    Price = findProductById.Price,
                    CreateAt = findProductById.CreateAt,
                    UpdateAt = findProductById.UpdateAt,
                    DeleteAt = findProductById.DeleteAt,
                    Status = findProductById.Status,
                    ProductImgs = findProductById.ProductImgs,
                };
                return new ApiSuccessResult<ProductDTO>(data);
            }
            catch (Exception)
            {
                return new ApiErrorResult<ProductDTO> { Message = "Loi"};
            }
        }

        public async Task<ApiResult<bool>> UnDeleteAsync(int id)
        {
            try
            {
                var findProductById = await _productRepository.GetById(id);
                if (findProductById.Status != 2)
                    return new ApiErrorResult<bool> { Message = "Tai khoan da duoc khoi phuc" };
                findProductById.Status = 1;
                await _productRepository.UpdateAsync(findProductById);
                return new ApiSuccessResult<bool> { Message = "Khoi phuc thanh cong" };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
