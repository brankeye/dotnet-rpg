using System.Threading.Tasks;
using dotnet_rpg.Service.Behaviors;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Decorators.Queries
{
    public class BehaviorQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;
        private readonly IBehaviorMediator _behaviorMediator;
        
        public BehaviorQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decoratee,
            IBehaviorMediator behaviorMediator)
        {
            _decoratee = decoratee;
            _behaviorMediator = behaviorMediator;
        }
        
        public async Task<TResult> HandleAsync(TQuery query)
        {
            await _behaviorMediator.HandleAsync(query);
            return await _decoratee.HandleAsync(query);
        }
    }
}