using System;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.EquipWeaponCommand
{
    public class EquipWeaponCommandValidator : Validator<EquipWeaponCommand>
    {
        public EquipWeaponCommandValidator()
        {
            RuleFor(x => x.CharacterId)
                .NotEmpty()
                .WithMessage("Character id must be given");
            RuleFor(x => x.WeaponId)
                .NotEmpty()
                .WithMessage("Weapon id must be given");
        }
    }
}