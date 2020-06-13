using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Auth.LoginQuery
{
    public class LoginQueryValidator : Validator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("A username must be given");
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage($"A password must be given");
        }
    }
}