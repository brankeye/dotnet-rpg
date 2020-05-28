using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.User.Dtos;
using dotnet_rpg.Api.Controllers.User.Mapper;
using dotnet_rpg.Service.Core.User;
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
        private readonly IUserMapper _userMapper;

        public UserController(IUserService userService) 
        {
            _userService = userService;
            _userMapper = new UserMapper();
        }

        [HttpGet]
        public async Task<ApiResponse<UserResponse>> Get()
        {
            var result = await _userService.GetAsync();
            var data = _userMapper.Map(result);
            return ApiResponse.Ok(data);
        }
    }
}