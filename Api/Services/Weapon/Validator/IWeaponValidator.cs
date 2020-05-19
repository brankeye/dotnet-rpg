using dotnet_rpg.Api.Services.Weapon.Dtos;
using dotnet_rpg.Api.Validation;

namespace dotnet_rpg.Api.Services.Weapon.Validator
{
    public interface IWeaponValidator
        : IValidator<CreateWeaponDto>,
          IValidator<UpdateWeaponDto>
    {
        
    }
}
