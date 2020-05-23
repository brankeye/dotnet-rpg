using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Api.Controllers.Auth.Mapper
{
    public class AuthMapper : IAuthMapper
    {
        public LoginResponse Map(LoginDto source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new LoginResponse
            {
                Token = source.Token
            };
        }

        public RegisterResponse Map(RegisterDto source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new RegisterResponse
            {
                Id = source.Id,
                Username = source.Username
            };
        }
    }
}