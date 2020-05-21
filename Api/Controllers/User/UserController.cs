using System;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Character.Dtos;
using dotnet_rpg.Api.Controllers.User.Dtos;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Character;
using dotnet_rpg.Service.Core.Character.Dtos;
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

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetAsync();
            return Ok(new UserResponse
            {
                Id = response.Id,
                Username = response.Username
            });
        }
    }
}