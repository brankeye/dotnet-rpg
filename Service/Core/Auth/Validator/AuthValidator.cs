using dotnet_rpg.Service.Core.Auth.Dtos;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Auth.Validator
{
    public class AuthValidator : IAuthValidator
    {
        private readonly Validation.IValidator<CredentialsDto> _credentialsValidator;

        public AuthValidator()
        {
            _credentialsValidator = new CredentialsDtoValidator();
        }
        
        public void ValidateAndThrow(CredentialsDto dto)
        {
            _credentialsValidator.ValidateAndThrow(dto);
        }
    }

    public class CredentialsDtoValidator : Validation.Validator<CredentialsDto>
    {
        public CredentialsDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("A username must be given");
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("A password must be given");
        }
    }
}
