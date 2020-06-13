using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Weapon.UpdateWeaponCommand
{
    public class UpdateWeaponCommandHandler : ICommandHandler<UpdateWeaponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWeaponCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(UpdateWeaponCommand command)
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            HandleUpdate(weapon, command);
            _unitOfWork.Weapons.Update(weapon);
        }
        
        private static void HandleUpdate(Domain.Models.Weapon weapon, UpdateWeaponCommand command)
        {
            weapon.Name = command.Name;
            weapon.Damage = command.Damage;
        }
    }
}