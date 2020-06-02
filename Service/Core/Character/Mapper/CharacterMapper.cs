using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Core.Skill.Dtos;
using dotnet_rpg.Service.Core.Skill.Mapper;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Mapper;

namespace dotnet_rpg.Service.Core.Character.Mapper
{
    public class CharacterMapper : ICharacterMapper
    {
        private readonly IWeaponMapper _weaponMapper;
        private readonly ISkillMapper _skillMapper;
        
        public CharacterMapper()
        {
            _weaponMapper = new WeaponMapper();
            _skillMapper = new SkillMapper();
        }

        public CharacterDto Map(Domain.Models.Character source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var weapon = source.Weapon != null ? _weaponMapper.Map(source.Weapon) : null;

            var skills = source.CharacterSkills
                .Select(characterSkill => _skillMapper.Map(characterSkill.Skill))
                .ToList();

            return new CharacterDto
            {
                Id = source.Id,
                Name = source.Name,
                HitPoints = source.HitPoints,
                Strength = source.Strength,
                Defense = source.Defense,
                Intelligence = source.Intelligence,
                Class = source.Class.ToString(),
                Weapon = weapon,
                Skills = skills
            };
        }

        public Domain.Models.Character Map(CreateCharacterDto source, Guid userId)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Domain.Models.Character
            {
                UserId = userId,
                Name = source.Name,
                HitPoints = 100,
                Strength = 1,
                Defense = 1,
                Intelligence = 1,
                Class = (RpgClass) Enum.Parse(typeof(RpgClass), source.Class)
            };
        }
    }
}