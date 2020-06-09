using System;
using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Operations.Auth.Operations.LoginQuery;
using dotnet_rpg.Service.Operations.Auth.Operations.RegisterCommand;
using dotnet_rpg.Service.Operations.User.Queries.UserQuery;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.Auth
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<LoginQueryResult>> Login(LoginQuery query)
        {
            var data = await _mediator.HandleAsync(query);
            return ApiResponse.Ok(data);
        }

        [HttpPost("register")]
        public async Task<ApiResponse<UserQueryResult>> Register(RegisterCommand command)
        {
            command.UserId = Guid.NewGuid();
            await _mediator.HandleAsync(command);
            var userQuery = new UserQuery
            {
                UserId = command.UserId
            };
            var user = await _mediator.HandleAsync(userQuery);
            return ApiResponse.Ok(user);
        }
    }
}