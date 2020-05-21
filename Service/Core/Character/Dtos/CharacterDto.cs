using System;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Service.Core.Character.Dtos
{
    public class CharacterDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int HitPoints { get; set; }
        
        public int Strength { get; set; }
        
        public int Defense { get; set; }
        
        public int Intelligence { get; set; }
        
        public string Class { get; set; }

        public WeaponDto Weapon { get; set; }
    }
}