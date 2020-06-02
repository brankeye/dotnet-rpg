using System;
using dotnet_rpg.Service.Core.User.Dtos;
using dotnet_rpg.Service.Core.User.Mapper;
using FluentAssertions;
using NUnit.Framework;

namespace dotnet_rpg.Service.Test.User
{
    [TestFixture]  
    public class UserMapperTests
    {
        private IUserMapper _userMapper;
        
        [SetUp]
        public void SetUp()
        {
            _userMapper = new UserMapper();
        }

        private static Domain.Models.User CreateUser()
        {
            return new dotnet_rpg.Domain.Models.User
            {
                Id = Guid.NewGuid(),
                Username = "jimothy",
                PasswordHash = new byte[1],
                PasswordSalt = new byte[2],
            };
        }
        
        
        [Test]
        public void Happy_MapUserToUserDto()
        {
            var user = CreateUser();
            var result = _userMapper.Map(user);
            var expected = new UserDto
            {
                Id = user.Id,
                Username = user.Username
            };
            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void Sad_MapUserToUserDto()
        {
            Domain.Models.User user = null;
            Assert.Throws<ArgumentNullException>(() => _userMapper.Map(user));
        }
    }
}