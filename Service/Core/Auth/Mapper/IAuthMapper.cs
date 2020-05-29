using System.IdentityModel.Tokens.Jwt;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Service.Core.Auth.Mapper
{
    public interface IAuthMapper
    {
        LoginDto Map(JwtSecurityToken token);
        
        RegisterDto Map(Domain.Models.User user);

        Domain.Models.User Map(CredentialsDto credentials, byte[] passwordHash, byte[] passwordSalt);
    }
}