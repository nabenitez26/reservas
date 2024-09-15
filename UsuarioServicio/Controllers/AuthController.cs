using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Reservas.Application.Services;

namespace UsuarioServicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var token = _authService.Authenticate(loginRequest.Email, loginRequest.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }
    }
}