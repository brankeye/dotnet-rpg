using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.DeleteCharacterCommand
{
    public class DeleteCharacterCommandHandler : ICommandHandler<DeleteCharacterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCharacterCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(DeleteCharacterCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            _unitOfWork.Characters.Delete(character);
        }
    }
}