using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Contracts.CQRS.Mediator
{
    public interface IMediator
    {
        Task<TResult> HandleAsync<TResult>(IQuery<TResult> query);
        
        Task HandleAsync(ICommand command);
    }
}