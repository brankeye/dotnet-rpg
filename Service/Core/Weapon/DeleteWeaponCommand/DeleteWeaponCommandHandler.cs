using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Core.Character.DeleteCharacterCommand;

namespace dotnet_rpg.Service.Core.Weapon.DeleteWeaponCommand
{
    public class DeleteWeaponCommandHandler : ICommandHandler<DeleteWeaponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWeaponCommandHandler(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task HandleAsync(DeleteWeaponCommand command)
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == command.UserId)
                .Where(x => x.Id == command.Id)
                .SingleAsync();
            _unitOfWork.Weapons.Delete(weapon);
        }
    }
}