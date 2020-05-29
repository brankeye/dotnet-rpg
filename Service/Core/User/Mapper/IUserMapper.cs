using dotnet_rpg.Service.Core.User.Dtos;

namespace dotnet_rpg.Service.Core.User.Mapper
{
    public interface IUserMapper
    {
        UserDto Map(Domain.Models.User user);
    }
}