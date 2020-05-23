using dotnet_rpg.Api.Controllers.Auth.Dtos;
using dotnet_rpg.Api.Mapper;
using dotnet_rpg.Service.Core.Auth.Dtos;

namespace dotnet_rpg.Api.Controllers.Auth.Mapper
{
    public interface IAuthMapper : 
        IMapper<LoginDto, LoginResponse>,
        IMapper<RegisterDto, RegisterResponse>
    {
        
    }
}