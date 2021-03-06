using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Utility.AuthUtility;

namespace dotnet_rpg.Service.Core.Auth.LoginQuery
{
    public class UserMapper : Mapper<CryptographicPassword, Domain.Models.User>
    {
        protected override Domain.Models.User HandleMapping(CryptographicPassword input)
        {
            return new Domain.Models.User
            {
                PasswordHash = input.PasswordHash,
                PasswordSalt = input.PasswordSalt
            };
        }
    }
}