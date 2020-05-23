using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Api.Controllers.Auth.Mapper;
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
        private readonly IAuthMapper _authMapper;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _authMapper = new AuthMapper();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });

            var response = _authMapper.Map(result);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });
            
            var response = _authMapper.Map(result);
            
            return Ok(response);
        }
    }
}