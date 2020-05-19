using System.Threading.Tasks;
using dotnet_rpg.Api.Dtos.Auth;
using dotnet_rpg.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CredentialsDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CredentialsDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return Ok(response);
        }
    }
}