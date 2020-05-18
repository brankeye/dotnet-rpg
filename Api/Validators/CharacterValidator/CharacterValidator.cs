using System;
using System.Collections.Generic;
using dotnet_rpg.Api.Dtos.Character;
using dotnet_rpg.Domain.Enums;

namespace dotnet_rpg.Api.Validators.CharacterValidator
{
    public class CharacterValidator : Validator, ICharacterValidator
    {
        public void Validate(CreateCharacterDto dto)
        {
            if (dto == null) 
            {
                throw new ArgumentNullException(nameof(dto));
            }
                    
            if (dto.Name == null) 
            {
                AddError("Character name is invalid");
            }

            try 
            {
                var _ = (RpgClass) Enum.Parse(typeof(RpgClass), dto.Class);
            } catch (Exception) 
            {
                var names = string.Join(", ", Enum.GetNames(typeof(RpgClass)));
                AddError($"Character class is invalid. Valid options are {names}");
            }

            if (!IsValid)
            {
                throw new ValidationException(Errors);
            }
        }

        public void Validate(UpdateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Name == null)
            {
                AddError("Character name is invalid");
            }

            try
            {
                var _ = (RpgClass)Enum.Parse(typeof(RpgClass), dto.Class);
            }
            catch (Exception)
            {
                var names = string.Join(", ", Enum.GetNames(typeof(RpgClass)));
                AddError($"Character class is invalid. Valid options are {names}");
            }

            if (!IsValid)
            {
                throw new ValidationException(Errors);
            }
        }
    }
}