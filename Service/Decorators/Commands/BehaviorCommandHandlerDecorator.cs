using System.Threading.Tasks;
using dotnet_rpg.Service.Behaviors;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Decorators.Commands
{
    public class BehaviorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly IBehaviorMediator _behaviorMediator;
        
        public BehaviorCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IBehaviorMediator behaviorMediator)
        {
            _decoratee = decoratee;
            _behaviorMediator = behaviorMediator;
        }
        
        public async Task HandleAsync(TCommand command)
        {
            await _behaviorMediator.HandleAsync(command);
            await _decoratee.HandleAsync(command);
        }
    }
}