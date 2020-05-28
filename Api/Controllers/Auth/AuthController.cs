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
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });

            var data = _authMapper.Map(result);
            
            return ApiResponse.Ok(data);
        }

        [HttpPost("register")]
        public async Task<ApiResponse<RegisterResponse>> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(new CredentialsDto
            {
                Username = request.Username,
                Password = request.Password
            });
            
            var data = _authMapper.Map(result);

            return ApiResponse.Ok(data);
        }
    }
}