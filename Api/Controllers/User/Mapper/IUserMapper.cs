using dotnet_rpg.Api.Controllers.User.Dtos;
using dotnet_rpg.Api.Mapper;
using dotnet_rpg.Service.Core.User.Dtos;

namespace dotnet_rpg.Api.Controllers.User.Mapper
{
    public interface IUserMapper : IMapper<UserDto, UserResponse>
    {
        
    }
}