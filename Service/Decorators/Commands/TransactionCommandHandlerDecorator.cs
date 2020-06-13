using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.Repository.Persister;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Decorators.Commands
{
    public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly IRepositoryPersister _persister;
        
        public TransactionCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IRepositoryPersister persister)
        {
            _decoratee = decoratee;
            _persister = persister;
        }
        
        public async Task HandleAsync(TCommand operation)
        {
            await _decoratee.HandleAsync(operation);
            await _persister.CommitAsync();
        }
    }
}