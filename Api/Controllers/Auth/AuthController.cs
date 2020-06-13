using System;
using System.Threading.Tasks;
using dotnet_rpg.Api.Controllers.Auth.Dto;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Core.Auth.LoginQuery;
using dotnet_rpg.Service.Core.Auth.RegisterCommand;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IOperationMediator _operationMediator;

        public AuthController(IOperationMediator operationMediator)
        {
            _operationMediator = operationMediator;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<LoginQueryResult>> Login(LoginRequest request)
        {
            var data = await _operationMediator.HandleAsync(new LoginQuery
            {
                Username = request.Username,
                Password = request.Password
            });
            return ApiResponse.Ok(data);
        }

        [HttpPost("register")]
        public async Task<ApiResponse> Register(RegisterRequest request)
        {
            await _operationMediator.HandleAsync(new RegisterCommand
            {
                Username = request.Username,
                Password = request.Password
            });
            return ApiResponse.Ok();
        }
    }
}