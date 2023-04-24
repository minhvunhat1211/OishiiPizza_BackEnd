using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oishii_pizza.Domain.Features.ProductService;
using oishii_pizza.Domain.Models.ProductModels;

namespace oishii_pizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(int TypeOfProductId, [FromForm]ProductCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _productService.CreateAsync(TypeOfProductId, request);
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
                var result = await _productService.GetByIdAsync(id);
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
                var result = await _productService.GetAll(pageSize, pageIndex, search);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("getallbytypeofproductname")]
        public async Task<IActionResult> FindAllByTypeOfProductName(int? pageSize, int? pageIndex, string typeOfProductName)
        {
            try
            {
                var result = await _productService.GetAllByTypeOfProductname(pageSize, pageIndex, typeOfProductName);
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
        public async Task<IActionResult> Edit(int id, [FromForm] ProductEditRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _productService.EditAsync(id, request);
                if (result.IsSuccessed)
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
    }
}
