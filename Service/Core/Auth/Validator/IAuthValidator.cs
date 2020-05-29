using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Validation;

namespace dotnet_rpg.Service.Core.Auth.Validator
{
    public interface IAuthValidator : IValidator<CredentialsDto>
    {
        
    }
}
