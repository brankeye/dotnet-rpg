using System;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.UpdateCharacterCommand
{
    public class UpdateCharacterCommandHandler : ICommandHandler<UpdateCharacterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCharacterCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(UpdateCharacterCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            HandleUpdate(character, command);
            _unitOfWork.Characters.Update(character);
        }
        
        private static void HandleUpdate(Domain.Models.Character character, UpdateCharacterCommand command)
        {
            character.Name = command.Name;
            character.Class = (RpgClass) Enum.Parse(typeof(RpgClass), command.Class);
        }
    }
}