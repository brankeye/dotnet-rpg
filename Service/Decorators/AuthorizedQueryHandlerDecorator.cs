using System;
using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Operations.Auth;

namespace dotnet_rpg.Service.Decorators
{
    public class AuthorizedQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IAuthorizedQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;
        private readonly IAuthContext _authContext;
        
        public AuthorizedQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decoratee,
            IAuthContext authContext)
        {
            _decoratee = decoratee;
            _authContext = authContext;
        }
        
        public Task<TResult> HandleAsync(TQuery operation)
        {
            if (operation.UserId == Guid.Empty)
            {
                operation.UserId = _authContext.UserId;
            }
            return _decoratee.HandleAsync(operation);
        }
    }
}