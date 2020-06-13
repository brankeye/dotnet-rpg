using System;
using System.Linq;
using dotnet_rpg.Service.Contracts.Mapping;
using dotnet_rpg.Service.Core.Skill.GetSkillQuery;
using dotnet_rpg.Service.Core.Weapon.GetWeaponQuery;

namespace dotnet_rpg.Service.Core.Character.GetCharacterQuery
{
    public class GetCharacterQueryResultMapper : IMapper<Domain.Models.Character, GetCharacterQueryResult>
    {
        private readonly IMapper<Domain.Models.Weapon, GetWeaponQueryResult> _weaponMapper;
        private readonly IMapper<Domain.Models.Skill, GetSkillQueryResult> _skillMapper;
        
        public GetCharacterQueryResultMapper(
            IMapper<Domain.Models.Weapon, GetWeaponQueryResult> weaponMapper,
            IMapper<Domain.Models.Skill, GetSkillQueryResult> skillMapper)
        {
            _weaponMapper = weaponMapper;
            _skillMapper = skillMapper;
        }
        
        public GetCharacterQueryResult Map(Domain.Models.Character input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var weapon = input.Weapon != null ? _weaponMapper.Map(input.Weapon) : null;

            var skills = input.CharacterSkills
                .Select(characterSkill => _skillMapper.Map(characterSkill.Skill))
                .ToList();

            return new GetCharacterQueryResult
            {
                Id = input.Id,
                Name = input.Name,
                HitPoints = input.HitPoints,
                Strength = input.Strength,
                Defense = input.Defense,
                Intelligence = input.Intelligence,
                Class = input.Class.ToString(),
                Weapon = weapon,
                Skills = skills
            };
        }
    }
}
