using System;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Weapon.Validator
{
    public class WeaponValidator : Validation.Validator, IWeaponValidator
    {
        public void Validate(CreateWeaponDto dto)
        {
            if (dto == null) 
            {
                throw new ArgumentNullException(nameof(dto));
            }
                    
            if (dto.Name == null) 
            {
                AddError("Weapon name is invalid");
            }

            if (dto.Damage <= 0) 
            {
                AddError("Weapon must do some damage");
            }

            if (!IsValid) 
            {
                throw new ValidationException(Errors);
            }
        }

        public void Validate(UpdateWeaponDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Name == null)
            {
                AddError("Weapon name is invalid");
            }

            if (dto.Damage <= 0)
            {
                AddError("Weapon must do some damage");
            }

            if (!IsValid)
            {
                throw new ValidationException(Errors);
            }
        }
    }
}