using System;
using dotnet_rpg.Api.Dtos.Weapon;

namespace dotnet_rpg.Api.Validators.WeaponValidator
{
    public interface IWeaponValidator
        : IValidator<CreateWeaponDto>,
          IValidator<UpdateWeaponDto>
    {
        
    }
}
