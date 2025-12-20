using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeShopLibrary.DTOs;
using ShoeShopLibrary.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthService service) : ControllerBase
    {
        private readonly AuthService _service = service;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> PostUser(LoginRequest loginRequest)
        {
            var token = await _service.AuthUserAsync(loginRequest);

            return token is null ? 
                BadRequest() : 
                Ok(token);
        }
    }
}
