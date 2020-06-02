using System.Threading.Tasks;
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
        public async Task<ApiResponse<LoginDto>> Login(CredentialsDto request)
        {
            var data = await _authService.LoginAsync(request);
            return ApiResponse.Ok(data);
        }

        [HttpPost("register")]
        public async Task<ApiResponse<RegisterDto>> Register(CredentialsDto request)
        {
            var data = await _authService.RegisterAsync(request);
            return ApiResponse.Ok(data);
        }
    }
}