using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Weapon.GetWeaponQuery
{
    public class GetWeaponQueryValidator : Validator<GetWeaponQuery>
    {
        public GetWeaponQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("An id must be given");
        }
    }
}