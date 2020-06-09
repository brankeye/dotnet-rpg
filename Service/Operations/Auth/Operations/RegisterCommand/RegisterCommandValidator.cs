using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Operations.Auth.Operations.RegisterCommand
{
    public class RegisterCommandValidator : Validator<RegisterCommand>
    {
        public RegisterCommandValidator()
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