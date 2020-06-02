using System;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Service.Core.Auth.Mapper;
using NUnit.Framework;

namespace Service.Test.Core.Auth.Mapper
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
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var result = _authMapper.Map(new User
            {
                Id = Guid.NewGuid(),
                Username = "jimothy"
            });
            Assert.That(result, Has.Property("Username").EqualTo("jimothy"));
        }
    }
}