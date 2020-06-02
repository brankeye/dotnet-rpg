using System;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Mapper;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using FluentAssertions;
using NUnit.Framework;

namespace dotnet_rpg.Service.Test.Character
{
    [TestFixture]
    public class CharacterMapperTests
    {
        private ICharacterMapper _characterMapper;

        [SetUp]
        public void SetUp()
        {
            _characterMapper = new CharacterMapper();
        }

        [Test]
        public void Happy_MapCharacterToCharacterDto()
        {
            var id = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var weaponId = Guid.NewGuid();
            var result = _characterMapper.Map(new Domain.Models.Character
            {
                Id = id,
                UserId = userId,
                Name = "Jimothy",
                Class = RpgClass.Knight,
                HitPoints = 100,
                Defense = 1,
                Strength = 1,
                Intelligence = 1,
                WeaponId = Guid.NewGuid(),
                Weapon = new Domain.Models.Weapon
                {
                    Id = weaponId,
                    UserId = userId,
                    Name = "Broad Sword",
                    Damage = 1,
                }
            });
            var expectedValue = new CharacterDto
            {
                Id = id,
                Name = "Jimothy",
                Class = "Knight",
                HitPoints = 100,
                Defense = 1,
                Strength = 1,
                Intelligence = 1,
                Weapon = new WeaponDto
                {
                    Id = weaponId,
                    Name = "Broad Sword",
                    Damage = 1,
                }
            };
            expectedValue.Should().BeEquivalentTo(result);
        }

        [Test]
        public void Happy_MapCreateCharacterDtoToCharacter()
        {
            var userId = Guid.NewGuid();
            var result = _characterMapper.Map(new CreateCharacterDto
            {
                Name = "Jimothy",
                Class = "Knight"
            }, userId);
            var expectedValue = new Domain.Models.Character
            {
                Id = Guid.Empty,
                UserId = userId,
                Name = "Jimothy",
                Class = RpgClass.Knight,
                HitPoints = 100,
                Defense = 1,
                Strength = 1,
                Intelligence = 1,
                Weapon = null,
            };
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Sad_MapCharacterToCharacterDto()
        {
            Domain.Models.Character character = null;
            Assert.Throws<ArgumentNullException>(() => _characterMapper.Map(character));
        }
        
        [Test]
        public void Sad_MapCreateCharacterDtoToCharacterDto()
        {
            CreateCharacterDto dto = null;
            Assert.Throws<ArgumentNullException>(() => _characterMapper.Map(dto, Guid.NewGuid()));
        }
    }
}