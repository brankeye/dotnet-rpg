using System.Threading.Tasks;
using dotnet_rpg.Service.Core.User;
using dotnet_rpg.Service.Core.User.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ApiResponse<UserDto>> Get()
        {
            var data = await _userService.GetAsync();
            return ApiResponse.Ok(data);
        }
    }
}