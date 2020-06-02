using System;
using dotnet_rpg.Service.Core.User.Dtos;

namespace dotnet_rpg.Service.Core.User.Mapper
{
    public class UserMapper : IUserMapper
    {
        public UserDto Map(Domain.Models.User source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new UserDto
            {
                Id = source.Id,
                Username = source.Username
            };
        }
    }
}