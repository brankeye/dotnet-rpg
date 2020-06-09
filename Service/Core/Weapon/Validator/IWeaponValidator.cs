using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Service.Core.Weapon.Validator
{
    public interface IWeaponValidator
    {
        void ValidateAndThrow(CreateWeaponDto entity);
        
        void ValidateAndThrow(UpdateWeaponDto entity);
    }
}
