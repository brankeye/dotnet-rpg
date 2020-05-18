using System;
using dotnet_rpg.Api.Dtos.Auth;

namespace dotnet_rpg.Api.Validators.AuthValidator
{
    public class AuthValidator : Validator, IAuthValidator
    {
        public void Validate(CredentialsDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (dto.Username == null)
            {
                AddError("A username must be given");
            }

            if (dto.Password == null)
            {
                AddError("A password must be given");
            }

            if (!IsValid)
            {
                throw new ValidationException(Errors);
            }
        }
    }
}
