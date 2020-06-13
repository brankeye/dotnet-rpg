using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.Context;

namespace dotnet_rpg.Service.Behaviors.Authorized
{
    public class AuthorizedBehaviorHandler : IBehaviorHandler<IAuthorizedBehavior>
    {
        private readonly IAuthContext _authContext;
        
        public AuthorizedBehaviorHandler(IAuthContext authContext)
        {
            _authContext = authContext;
        }

        public Task HandleAsync(IAuthorizedBehavior value)
        {
            value.UserId = _authContext.UserId;
            
            return Task.CompletedTask;
        }
    }
}