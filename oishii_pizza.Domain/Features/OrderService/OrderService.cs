using oishii_pizza.Domain.Common.APIResponse;
using oishii_pizza.Domain.Common.Paging;
using oishii_pizza.Domain.Models.OrderModels;
using oishii_pizza.Domain.Models.ProductModels;
using oishii_pizza.Domain.Models.TypeOfProductModels;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.OrderRepository;
using oishii_pizza.Infrastructure.Repositories.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Domain.Features.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ApiResult<OrderDTO>> CreateAsync(OrderCreateRequest createRequest)
        {
            try
            {
                var newOrder = new Order()
                {
                    Title = createRequest.Title,
                    Note = createRequest.Note,
                    NameCustomer = createRequest.NameCustomer,
                    PhoneNumberCustomer = createRequest.PhoneNumberCustomer,
                    AddressCustomer = createRequest.AddressCustomer,
                    TotalPrice = createRequest.TotalPrice,
                    CreateAt = DateTime.Now,
                    Status = 1
                };
                var data = new OrderDTO()
                {
                    Id = newOrder.Id,
                    Title = newOrder.Title,
                    Note = newOrder.Note,
                    NameCustomer = newOrder.NameCustomer,
                    PhoneNumberCustomer = newOrder.PhoneNumberCustomer,
                    AddressCustomer = newOrder.AddressCustomer,
                    TotalPrice = newOrder.TotalPrice,
                    Status = newOrder.Status,
                    CreateAt =  newOrder.CreateAt,
                    UpdateAt = newOrder.UpdateAt,
                    DeleteAt = newOrder.DeleteAt,
                };
                await _orderRepository.CreateAsync(newOrder);
                return new ApiSuccessResult<OrderDTO>(data);
            }
            catch (Exception)
            {
                return new ApiErrorResult<OrderDTO>("Loi");
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(int id)
        {
            try
            {
                var findOrderById = await _orderRepository.GetById(id);
                if (findOrderById.Status != 1)
                    return new ApiErrorResult<bool> { Message = "Order da duoc xoa" };
                findOrderById.Status = 2;
                findOrderById.DeleteAt = DateTime.Now;
                await _orderRepository.UpdateAsync(findOrderById);
                return new ApiSuccessResult<bool> { Message = "Xoa thanh cong" };
            }
            catch (Exception)
            {

                return new ApiErrorResult<bool> { Message = "Xoa that bai" };
            }
        }

        public async Task<ApiResult<bool>> EditAsync(int id, OrderEditRequest request)
        {
            try
            {
                var findOderById = await _orderRepository.GetById(id);
                findOderById.NameCustomer = request.NameCustomer;
                findOderById.AddressCustomer = request.AddressCustomer;
                findOderById.PhoneNumberCustomer = request.PhoneNumberCustomer;
                findOderById.Title = request.Title;
                findOderById.Note = request.Title;
                findOderById.UpdateAt = DateTime.Now;
                await _orderRepository.UpdateAsync(findOderById);
                return new ApiSuccessResult<bool> { Message = "Thanh cong" };
            }
            catch (Exception)
            {

                return new ApiErrorResult<bool> { Message = "That bai" };
            }
        }

        public async Task<ApiResult<PagedResult<OrderDTO>>> GetAll(int? pageSize, int? pageIndex, string search)
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
                var totalRow = await _orderRepository.CountAsync();
                var query = await _orderRepository.GetAll(pageSize, pageIndex);
                if (!string.IsNullOrEmpty(search))
                {
                    Expression<Func<Order, bool>> expression = x => x.NameCustomer.Contains(search);
                    query = await _orderRepository.GetAll(pageSize, pageIndex, expression);
                    totalRow = await _orderRepository.CountAsync(expression);
                }
                //Paging
                var data = query
                    .Select(x => new OrderDTO()
                    {
                        NameCustomer = x.NameCustomer,
                        PhoneNumberCustomer = x.PhoneNumberCustomer,
                        AddressCustomer = x.AddressCustomer,
                        Note = x.Note,
                        Title = x.Title,
                        TotalPrice = x.TotalPrice,
                        CreateAt = x.CreateAt,
                        UpdateAt = x.UpdateAt,
                        DeleteAt = x.DeleteAt,
                        Id = x.Id,
                        Status = x.Status,
                        //ProductImgs = x.ProductImgs, sau nay se la order detail
                    }).OrderByDescending(x => x.CreateAt).ToList();
                var pagedResult = new PagedResult<OrderDTO>()
                {
                    TotalRecord = totalRow,
                    PageSize = pageSize.Value,
                    PageIndex = pageIndex.Value,
                    Items = data
                };
                if (pagedResult == null)
                {
                    return new ApiErrorResult<PagedResult<OrderDTO>>("Khong co data");
                }
                return new ApiSuccessResult<PagedResult<OrderDTO>>(pagedResult);
            }
            catch (Exception)
            {

                return new ApiErrorResult<PagedResult<OrderDTO>>("Loi");
            }
        }

        public async Task<ApiResult<OrderDTO>> GetByIdAsync(int id)
        {
            try
            {
                var findOrderById = await _orderRepository.GetById(id);
                if (findOrderById == null)
                {
                    return new ApiErrorResult<OrderDTO>("Khong ton tai hoa don");
                }
                var data = new OrderDTO()
                {
                    Id = findOrderById.Id,
                    Title = findOrderById.Title,
                    Note = findOrderById.Note,
                    NameCustomer = findOrderById.NameCustomer,
                    PhoneNumberCustomer = findOrderById.PhoneNumberCustomer,
                    AddressCustomer = findOrderById.AddressCustomer,
                    TotalPrice = findOrderById.TotalPrice,
                    CreateAt = findOrderById.CreateAt,
                    UpdateAt = findOrderById.UpdateAt,
                    DeleteAt = findOrderById.DeleteAt,
                    Status = findOrderById.Status
                };
                return new ApiSuccessResult<OrderDTO>(data);
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
                var findOrderById = await _orderRepository.GetById(id);
                if (findOrderById.Status != 2)
                    return new ApiErrorResult<bool> { Message = "Order da duoc khoi phuc" };
                findOrderById.Status = 1;
                await _orderRepository.UpdateAsync(findOrderById);
                return new ApiSuccessResult<bool> { Message = "Khoi phuc thanh cong" };
            }
            catch (Exception)
            {

                return new ApiErrorResult<bool> { Message = "Khoi phuc that bai" };
            }
        }
    }
}
