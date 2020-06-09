using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Operations.User.Queries.UserQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<UserQueryResult>> Get()
        {
            var data = await _mediator.HandleAsync(new UserQuery());
            return ApiResponse.Ok(data);
        }
    }
}