using System;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Core.Character.Dtos;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.Validator
{
    public class CharacterValidator : ICharacterValidator
    {
        private readonly Validation.IValidator<CreateCharacterDto> _createCharacterDtoValidator;
        private readonly Validation.IValidator<UpdateCharacterDto> _updateCharacterDtoValidator;

        public CharacterValidator()
        {
            _createCharacterDtoValidator = new CreateCharacterDtoValidator();
            _updateCharacterDtoValidator = new UpdateCharacterDtoValidator();
        }
        
        public void ValidateAndThrow(CreateCharacterDto dto)
        {
            _createCharacterDtoValidator.ValidateAndThrow(dto);
        }

        public void ValidateAndThrow(UpdateCharacterDto dto)
        {
            _updateCharacterDtoValidator.ValidateAndThrow(dto);
        }
    }

    public class CreateCharacterDtoValidator : Validation.Validator<CreateCharacterDto>
    {
        public CreateCharacterDtoValidator()
        {
            var rpgClassList = string.Join(", ", Enum.GetNames(typeof(RpgClass)));
            
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Character name is invalid");
            RuleFor(x => x.Class)
                .IsEnumName(typeof(RpgClass))
                .WithMessage($"Character class is invalid. Valid options are {rpgClassList}");
        }
    }
    
    public class UpdateCharacterDtoValidator : Validation.Validator<UpdateCharacterDto>
    {
        public UpdateCharacterDtoValidator()
        {
            var rpgClassList = string.Join(", ", Enum.GetNames(typeof(RpgClass)));
            
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Character name is invalid");
            RuleFor(x => x.Class)
                .IsEnumName(typeof(RpgClass))
                .WithMessage($"Character class is invalid. Valid options are {rpgClassList}");
        }
    }
}