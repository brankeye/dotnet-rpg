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
                return null;
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
                return null;
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
                return null;
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