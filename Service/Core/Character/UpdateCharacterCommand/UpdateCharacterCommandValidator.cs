using System;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.UpdateCharacterCommand
{
    public class UpdateCharacterCommandValidator : Validator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
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