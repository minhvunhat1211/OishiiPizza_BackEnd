using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oishii_pizza.Domain.Features.OrderService;
using oishii_pizza.Domain.Features.ProductService;
using oishii_pizza.Domain.Models.OrderModels;
using oishii_pizza.Domain.Models.TypeOfProductModels;

namespace oishii_pizza.API.Controllers
{
    [Route("api/v1/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(OrderCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _orderService.CreateAsync(request);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> FindById(int id)
        {
            try
            {
                var result = await _orderService.GetByIdAsync(id);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> FindAll(int? pageSize, int? pageIndex, string? search)
        {
            try
            {
                var result = await _orderService.GetAll(pageSize, pageIndex, search);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("edit")]
        public async Task<IActionResult> Edit(int id, OrderEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _orderService.EditAsync(id, request);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _orderService.DeleteAsync(id);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception)
            {

                return BadRequest($"Could not delete {id}");
            }
        }
        [HttpDelete("undelete")]
        public async Task<IActionResult> UnDelete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _orderService.UnDeleteAsync(id);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception)
            {

                return BadRequest($"Could not un delete {id}");
            }
        }
    }
}
