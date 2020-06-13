using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Weapon.DeleteWeaponCommand
{
    public class DeleteWeaponCommandValidator : Validator<DeleteWeaponCommand>
    {
        public DeleteWeaponCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("An id must be given");
        }
    }
}