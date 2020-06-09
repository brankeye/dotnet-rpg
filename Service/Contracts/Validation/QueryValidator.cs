using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Operations;

namespace dotnet_rpg.Service.Contracts.Validation
{
    public abstract class QueryValidator<TQuery, TResult> : 
        Validator<TQuery>, 
        IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _handler;

        public QueryValidator(IQueryHandler<TQuery, TResult> handler)
        {
            _handler = handler;
        }
        
        public Task<TResult> HandleAsync(TQuery query)
        {
            ValidateAndThrow(query);

            return _handler.HandleAsync(query);
        }
    }
}