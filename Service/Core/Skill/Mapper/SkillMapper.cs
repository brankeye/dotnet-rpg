using System;
using dotnet_rpg.Service.Core.Skill.Dtos;

namespace dotnet_rpg.Service.Core.Skill.Mapper
{
    public class SkillMapper : ISkillMapper
    {
        public SkillDto Map(Domain.Models.Skill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Damage = skill.Damage,
            };
        }

        public Domain.Models.Skill Map(CreateSkillDto skill, Guid userId)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            return new Domain.Models.Skill
            {
                UserId = userId,
                Name = skill.Name,
                Damage = skill.Damage
            };
        }
    }
}