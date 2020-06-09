using System.IdentityModel.Tokens.Jwt;

namespace dotnet_rpg.Service.Utility.AuthUtility
{
    public interface IAuthUtility
    {
        JwtSecurityToken CreateToken(Domain.Models.User user);

        CryptographicPassword CreatePasswordHash(string password);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}