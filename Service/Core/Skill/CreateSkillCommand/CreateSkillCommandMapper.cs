using System;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Skill.CreateSkillCommand
{
    public class CreateWeaponCommandMapper : IMapper<CreateSkillCommand, Domain.Models.Skill>
    {
        public Domain.Models.Skill Map(CreateSkillCommand input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new Domain.Models.Skill
            {
                UserId = input.UserId,
                Id = input.GeneratedId,
                Name = input.Name,
                Damage = input.Damage
            };
        }
    }
}