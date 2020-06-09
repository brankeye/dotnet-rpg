using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Operations;

namespace dotnet_rpg.Service.Decorators
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decoratee;
        private readonly IValidator<TQuery> _validator;
        
        public ValidationQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decoratee,
            IValidator<TQuery> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }
        
        public Task<TResult> HandleAsync(TQuery operation)
        {
            _validator.ValidateAndThrow(operation);
            return _decoratee.HandleAsync(operation);
        }
    }
}