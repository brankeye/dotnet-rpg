using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Mediator;
using dotnet_rpg.Service.Core.User.GetUserQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Api.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IOperationMediator _operationMediator;

        public UserController(IOperationMediator operationMediator)
        {
            _operationMediator = operationMediator;
        }

        [HttpGet]
        public async Task<ApiResponse<GetUserQueryResult>> Get()
        {
            var data = await _operationMediator.HandleAsync(new GetUserQuery());
            return ApiResponse.Ok(data);
        }
    }
}