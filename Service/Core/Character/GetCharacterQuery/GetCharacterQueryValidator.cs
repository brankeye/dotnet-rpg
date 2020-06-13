using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Core.User.GetUserQuery;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.GetCharacterQuery
{
    public class GetCharacterQueryValidator : Validator<GetCharacterQuery>
    {
        public GetCharacterQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("An id must be given");
        }
    }
}