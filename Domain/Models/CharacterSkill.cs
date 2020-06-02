using System;

namespace dotnet_rpg.Domain.Models
{
    public class CharacterSkill
    {
        public Guid CharacterId { get; set; }
        
        public Character Character { get; set; }
        
        public Guid SkillId { get; set; }
        
        public Skill Skill { get; set; }
    }
}