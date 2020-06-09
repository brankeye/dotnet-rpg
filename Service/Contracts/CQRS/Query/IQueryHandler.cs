using System.Threading.Tasks;

namespace dotnet_rpg.Service.Contracts.CQRS.Query
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}