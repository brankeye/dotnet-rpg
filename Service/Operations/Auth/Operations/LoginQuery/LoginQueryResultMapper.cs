using System.IdentityModel.Tokens.Jwt;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Operations.Auth.Operations.LoginQuery
{
    public class LoginQueryResultMapper : IMapper<JwtSecurityToken, LoginQueryResult> 
    {
        public LoginQueryResult Map(JwtSecurityToken input)
        {
            return new LoginQueryResult
            {
                Token = input.RawData
            };
        }
    }
}