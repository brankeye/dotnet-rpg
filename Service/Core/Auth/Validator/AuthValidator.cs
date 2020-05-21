using System;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Auth.Validator
{
    public class AuthValidator : Validation.Validator, IAuthValidator
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
