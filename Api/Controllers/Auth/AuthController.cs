using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth;
using dotnet_rpg.Service.Core.Auth.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Auth
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
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });
            
            return Ok(new LoginResponse {
                Token = response.Token
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });
            
            return Ok(new RegisterResponse
            {
                Id = response.Id,
                Username = response.Username
            });
        }
    }
}