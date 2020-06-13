using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character.UnequipWeaponCommand
{
    public class UnequipWeaponCommandHandler : ICommandHandler<UnequipWeaponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnequipWeaponCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(UnequipWeaponCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.CharacterId)
                .SingleAsync();

            if (character.WeaponId == null)
            {
                throw new ServiceException("Character has not equipped a weapon");
            }

            character.WeaponId = null;
            character.Weapon = null;
            _unitOfWork.Characters.Update(character);
        }
    }
}