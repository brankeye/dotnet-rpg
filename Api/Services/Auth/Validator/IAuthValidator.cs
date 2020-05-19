using dotnet_rpg.Api.Dtos.Auth;
using dotnet_rpg.Api.Validation;

namespace dotnet_rpg.Api.Services.Auth.Validator
{
    public interface IAuthValidator : IValidator<CredentialsDto>
    {
    }
}
