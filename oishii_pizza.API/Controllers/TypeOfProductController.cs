﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oishii_pizza.Domain.Features.TypeOfProductService;
using oishii_pizza.Domain.Models.TypeOfProductModels;
using oishii_pizza.Domain.Models.UserModels;

namespace oishii_pizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfProductController : ControllerBase
    {
        private readonly ITypeOfProductService _typeOfProductService;
        public TypeOfProductController(ITypeOfProductService typeOfProductService)
        {
            _typeOfProductService = typeOfProductService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(TypeOfProductCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _typeOfProductService.CreateAsync(request);
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
                var result = await _typeOfProductService.GetByIdAsync(id);
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
                var result = await _typeOfProductService.GetAll(pageSize, pageIndex, search);
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
        public async Task<IActionResult> Edit(int id, TypeOfProductEditRequest resquest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _typeOfProductService.EditAsync(id, resquest);
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
