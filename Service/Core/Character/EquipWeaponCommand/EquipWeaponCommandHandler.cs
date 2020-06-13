using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.EquipWeaponCommand
{
    public class EquipWeaponCommandHandler : ICommandHandler<EquipWeaponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EquipWeaponCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(EquipWeaponCommand command)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.CharacterId)
                .SingleAsync();
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.WeaponId)
                .SingleAsync();

            character.WeaponId = weapon.Id;
            character.Weapon = weapon;
            
            _unitOfWork.Characters.Update(character);
        }
    }
}