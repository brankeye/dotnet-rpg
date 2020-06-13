using System;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Skill.GetSkillQuery
{
    public class GetSkillQueryResultMapper : IMapper<Domain.Models.Skill, GetSkillQueryResult>
    {
        public GetSkillQueryResult Map(Domain.Models.Skill input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new GetSkillQueryResult
            {
                Id = input.Id,
                Name = input.Name,
                Damage = input.Damage
            };
        }
    }
}
