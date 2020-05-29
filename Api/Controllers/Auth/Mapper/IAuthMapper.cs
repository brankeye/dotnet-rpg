using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Api.Controllers.Auth.Mapper
{
    public interface IAuthMapper
    {
        LoginResponse Map(LoginDto dto);

        RegisterResponse Map(RegisterDto dto);
    }
}