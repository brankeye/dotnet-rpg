using System;
using dotnet_rpg.Api.Dtos.Auth;

namespace dotnet_rpg.Api.Validators.AuthValidator
{
    public interface IAuthValidator
        : IValidator<CredentialsDto>
    {
    }
}
