using System.Threading.Tasks;
using dotnet_rpg.Dtos.Auth;
using dotnet_rpg.Models;
using dotnet_rpg.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAuthDto request) {
            var serviceResponse = await _authService.RegisterAsync(request.Username, request.Password);
            return Ok(serviceResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RegisterAuthDto request) {
            var serviceResponse = await _authService.LoginAsync(request.Username, request.Password);
            return Ok(serviceResponse);
        }
    }
}