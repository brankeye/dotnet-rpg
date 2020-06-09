using System.Threading.Tasks;

namespace dotnet_rpg.Service.Contracts.CQRS.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}