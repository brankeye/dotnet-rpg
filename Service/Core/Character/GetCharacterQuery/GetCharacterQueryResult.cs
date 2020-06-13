using System;
using System.Collections.Generic;
using dotnet_rpg.Service.Core.Skill.GetSkillQuery;
using dotnet_rpg.Service.Core.Weapon.GetWeaponQuery;

namespace dotnet_rpg.Service.Core.Character.GetCharacterQuery
{
    public class GetCharacterQueryResult
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int HitPoints { get; set; }
        
        public int Strength { get; set; }
        
        public int Defense { get; set; }
        
        public int Intelligence { get; set; }
        
        public string Class { get; set; }

        public GetWeaponQueryResult Weapon { get; set; }
        
        public IEnumerable<GetSkillQueryResult> Skills { get; set; }
    }
}