using System;
using System.Collections.Generic;
using dotnet_rpg.Dtos;
using dotnet_rpg.Enums;
using dotnet_rpg.Exceptions;

namespace dotnet_rpg.Validators
{
    public class CharacterValidator
    {
        public void Validate(WriteCharacterDto dto) {
            if (dto == null) {
                throw new ArgumentException(
                    message: "shape is not a recognized shape",
                    paramName: nameof(dto)
                );
            }

            var messages = new List<ValidationError>();
                    
            if (dto.Name == null) {
                messages.Add(new ValidationError("Character name is invalid"));
            }

            try {
                var _ = (RpgClass) Enum.Parse(typeof(RpgClass), dto.Class);
            } catch (Exception) {
                var names = string.Join(", ", Enum.GetNames(typeof(RpgClass)));
                messages.Add(new ValidationError($"Character class is invalid. Valid options are {names}"));
            }

            if (messages.Count > 0) {
                throw new ValidationException(messages);
            }
        }
    }
}