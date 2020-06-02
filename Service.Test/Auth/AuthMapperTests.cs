using System;
using System.IdentityModel.Tokens.Jwt;
using dotnet_rpg.Service.Core.Auth.Dtos;
using dotnet_rpg.Service.Core.Auth.Mapper;
using FluentAssertions;
using NUnit.Framework;

namespace dotnet_rpg.Service.Test.Auth
{
    [TestFixture]  
    public class AuthMapperTests
    {
        private IAuthMapper _authMapper;
        
        [SetUp]
        public void SetUp()
        {
            _authMapper = new AuthMapper();
        }

        [Test]
        public void Happy_MapUserToRegisterDto()
        {
            var id = Guid.NewGuid();
            const string username = "jimothy";
            var result = _authMapper.Map(new dotnet_rpg.Domain.Models.User
            {
                Id = id,
                Username = username
            });
            var expectedValue = new RegisterDto
            {
                Id = id,
                Username = username
            };
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Happy_MapCredentialsToUserDto()
        {
            var credentials = new CredentialsDto
            {
                Username = "jimothy"
            };
            var passwordHash = new byte[] {1};
            var passwordSalt = new byte[] {2};
            var result = _authMapper.Map(credentials, passwordHash, passwordSalt);
            var expectedValue = new dotnet_rpg.Domain.Models.User
            {
                Username = "jimothy",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Happy_MapJwtSecurityTokenToLoginDto()
        {
            const string jwt = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhZGM5ZWFiMS1mYjAxLTRhNWEtOTE3NC04ZDNhMmRmNmNlZDkiLCJ1bmlxdWVfbmFtZSI6ImppbW90aHkiLCJuYmYiOjE1OTA3NjQ2ODcsImV4cCI6MTU5MDg1MTA4NywiaWF0IjoxNTkwNzY0Njg3fQ.uzYPNvkFfyz6qKMYjvVC2aW_uxLrIjpUWzMyCgyWdNtXXXnZgp6CnxWCC1k9CwW6bJHga53HIQ-t8n4B0qoqLw";
            var token = new JwtSecurityToken(jwt);
            var result = _authMapper.Map(token);
            var expectedValue = new LoginDto
            {
                Token = jwt
            };
            expectedValue.Should().BeEquivalentTo(result);
        }

        [Test]
        public void Sad_MapUserToRegisterDto()
        {
            Domain.Models.User user = null;
            Assert.Throws<ArgumentNullException>(() => _authMapper.Map(user));
        }
        
        [Test]
        public void Sad_MapCredentialsToUserDto()
        {
            CredentialsDto nil = null;
            var dto = new CredentialsDto();
            var passwordHash = new byte[] {1};
            var passwordSalt = new byte[] {2};
            Assert.Throws<ArgumentNullException>(() => _authMapper.Map(nil, passwordHash, passwordSalt));
            Assert.Throws<ArgumentNullException>(() => _authMapper.Map(dto, null, passwordSalt));
            Assert.Throws<ArgumentNullException>(() => _authMapper.Map(dto, passwordHash, null));
        }
        
        [Test]
        public void Sad_MapJwtSecurityTokenToLoginDto()
        {
            JwtSecurityToken token = null;
            Assert.Throws<ArgumentNullException>(() => _authMapper.Map(token));
        }
    }
}
