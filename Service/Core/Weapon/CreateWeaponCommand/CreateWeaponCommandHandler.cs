using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Contracts.CQRS.Command;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Character.CreateCharacterCommand;

namespace dotnet_rpg.Service.Core.Weapon.CreateWeaponCommand
{
    public class CreateWeaponCommandHandler : ICommandHandler<CreateWeaponCommand>
    {
        private readonly IMapper<CreateWeaponCommand, Domain.Models.Weapon> _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateWeaponCommandHandler(
            IMapper<CreateWeaponCommand, Domain.Models.Weapon> mapper,
            IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public Task HandleAsync(CreateWeaponCommand command)
        {
            var newWeapon = _mapper.Map(command);
            _unitOfWork.Weapons.Create(newWeapon);
            return Task.CompletedTask;
        }
    }
}