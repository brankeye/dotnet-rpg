using System;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character.Validator
{
    public class CharacterValidator : Validation.Validator, ICharacterValidator
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