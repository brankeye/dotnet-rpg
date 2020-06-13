using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.UnequipWeaponCommand
{
    public class UnequipWeaponCommandValidator : Validator<UnequipWeaponCommand>
    {
        public UnequipWeaponCommandValidator()
        {
            RuleFor(x => x.CharacterId)
                .NotEmpty()
                .WithMessage("Character id must be given");
        }
    }
}