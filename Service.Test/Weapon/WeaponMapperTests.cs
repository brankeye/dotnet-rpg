using System;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Mapper;
using FluentAssertions;
using NUnit.Framework;

namespace dotnet_rpg.Service.Test.Weapon
{
    [TestFixture]
    public class WeaponMapperTests
    {
        private IWeaponMapper _weaponMapper;

        [SetUp]
        public void SetUp()
        {
            _weaponMapper = new WeaponMapper();
        }

        private static Domain.Models.Weapon HydrateWeapon(Guid id, Guid userId)
        {
            return new Domain.Models.Weapon
            {
                Id = id,
                UserId = userId,
                Name = "Broad Sword",
                Damage = 1
            };
        }
        
        private static WeaponDto HydrateWeaponDto(Guid id)
        {
            return new WeaponDto
            {
                Id = id,
                Name = "Broad Sword",
                Damage = 1
            };
        }
        
        private static CreateWeaponDto HydrateCreateWeaponDto(Guid id)
        {
            return new CreateWeaponDto
            {
                Name = "Broad Sword",
                Damage = 1
            };
        }
        
        private static UpdateWeaponDto HydrateUpdateWeaponDto()
        {
            return new UpdateWeaponDto
            {
                Name = "Broad Sword",
                Damage = 1
            };
        }

        [Test]
        public void Happy_MapModelToDto()
        {
            var weapon = HydrateWeapon(Guid.NewGuid(), Guid.Empty);
            var result = _weaponMapper.Map(weapon);
            var expectedValue = HydrateWeaponDto(weapon.Id);
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Happy_MapCreateDtoToModel()
        {
            var userId = Guid.NewGuid();
            var dto = HydrateCreateWeaponDto(Guid.NewGuid());
            var result = _weaponMapper.Map(dto, userId);
            var expectedValue = HydrateWeapon(Guid.Empty, userId);
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Happy_MapUpdateDtoToModel()
        {
            var dto = HydrateUpdateWeaponDto();
            var result = _weaponMapper.Map(dto);
            var expectedValue = HydrateWeapon(Guid.Empty, Guid.Empty);
            expectedValue.Should().BeEquivalentTo(result);
        }
        
        [Test]
        public void Sad_MapCreateDtoToModel()
        { 
            CreateWeaponDto weapon = null;
            Assert.Throws<ArgumentNullException>(() => _weaponMapper.Map(weapon, Guid.NewGuid()));
        }
        
        [Test]
        public void Sad_MapUpdateDtoToModel()
        {
            UpdateWeaponDto weapon = null;
            Assert.Throws<ArgumentNullException>(() => _weaponMapper.Map(weapon));
        }
    }
}