using System;
using System.IdentityModel.Tokens.Jwt;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Service.Core.Auth.Mapper
{
    public class AuthMapper : IAuthMapper
    {
        public RegisterDto Map(Domain.Models.User source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new RegisterDto
            {
                Id = source.Id,
                Username = source.Username
            };
        }

        public LoginDto Map(JwtSecurityToken token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            return new LoginDto
            {
                Token = token.RawData
            };
        }

        public Domain.Models.User Map(CredentialsDto credentials, byte[] passwordHash, byte[] passwordSalt)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }
            
            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            } 
            
            if (passwordSalt == null)
            {
                throw new ArgumentNullException(nameof(credentials));
            }

            return new Domain.Models.User
            {
                Username = credentials.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }
    }
}