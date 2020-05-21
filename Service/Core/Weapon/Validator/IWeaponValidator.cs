using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Validation;

namespace dotnet_rpg.Service.Core.Weapon.Validator
{
    public interface IWeaponValidator
        : IValidator<CreateWeaponDto>,
          IValidator<UpdateWeaponDto>
    {
        
    }
}
