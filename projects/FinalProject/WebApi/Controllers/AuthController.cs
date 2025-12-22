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
        private readonly AuthService _service = service; // объект сервиса для работы с авторизацией

        [AllowAnonymous] // Атрибут дающий доступ любому неавторизованному пользователю
        [HttpPost("login")] // Метод Post с конечной точкой login
        public async Task<ActionResult<string>> PostUser(LoginRequest loginRequest)
        {
            var token = await _service.AuthUserWithTokenAsync(loginRequest); // атворизация с получением токена

            return token is null ? 
                BadRequest() : 
                Ok(token); // Если токен успешно сгенерирован возвращает его, в противном случае возвращает код 400 (Ошибка запроса)
        }
    }
}
