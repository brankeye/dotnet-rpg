using dotnet_rpg.Api.Controllers.User.Dtos;
using dotnet_rpg.Service.Core.User.Dtos;

namespace dotnet_rpg.Api.Controllers.User.Mapper
{
    public class UserMapper : IUserMapper
    {
        public UserResponse Map(UserDto source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new UserResponse
            {
                Id = source.Id,
                Username = source.Username
            };
        }
    }
}