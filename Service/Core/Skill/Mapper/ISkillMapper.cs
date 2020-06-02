using System;
using dotnet_rpg.Service.Core.Skill.Dtos;

namespace dotnet_rpg.Service.Core.Skill.Mapper
{
    public interface ISkillMapper
    {
        SkillDto Map(Domain.Models.Skill weapon);

        Domain.Models.Skill Map(CreateSkillDto dto, Guid userId);
    }
}