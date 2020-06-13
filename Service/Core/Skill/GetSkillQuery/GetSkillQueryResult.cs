using System;

namespace dotnet_rpg.Service.Core.Skill.GetSkillQuery
{
    public class GetSkillQueryResult
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int Damage { get; set; }
    }
}