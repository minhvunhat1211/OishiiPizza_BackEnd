using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using oishii_pizza.Domain.Features.UserService;
using oishii_pizza.Domain.Models.UserModels;

namespace oishii_pizza.API.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _userService.LoginAsync(request);
                if (result.IsSuccessed)
                    return Ok(result);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _userService.CreateAsync(request);
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
                var result = await _userService.GetByIdAsync(id);
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
                var result = await _userService.GetAll(pageSize, pageIndex, search);
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
        public async Task<IActionResult> Edit(int id, UserEditResquest resquest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _userService.EditAsync(id, resquest);
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
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var result = await _userService.DeleteAsync(id);
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
                var result = await _userService.UnDeleteAsync(id);
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
