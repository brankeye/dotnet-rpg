using System;

namespace dotnet_rpg.Service.Core.Skill.Dtos
{
    public class SkillDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}
