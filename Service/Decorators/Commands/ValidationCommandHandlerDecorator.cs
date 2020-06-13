using System.Threading.Tasks;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.Validation;

namespace dotnet_rpg.Service.Decorators.Commands
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _decoratee;
        private readonly IValidator<TCommand> _validator;
        
        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratee,
            IValidator<TCommand> validator)
        {
            _decoratee = decoratee;
            _validator = validator;
        }
        
        public async Task HandleAsync(TCommand operation)
        {
            _validator.ValidateAndThrow(operation);
            await _decoratee.HandleAsync(operation);
        }
    }
}