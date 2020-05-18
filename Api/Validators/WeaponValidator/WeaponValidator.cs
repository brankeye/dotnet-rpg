using System;
using System.Collections.Generic;
using dotnet_rpg.Api.Dtos.Weapon;
using dotnet_rpg.Infrastructure.Exceptions;

namespace dotnet_rpg.Api.Validators.WeaponValidator
{
    public class WeaponValidator : Validator, IWeaponValidator
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