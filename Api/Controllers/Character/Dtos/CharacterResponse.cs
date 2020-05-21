using System;
using dotnet_rpg.Api.Controllers.Weapon.Dtos;

namespace dotnet_rpg.Api.Controllers.Character.Dtos
{
    public class CharacterResponse
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public int HitPoints { get; set; }
        
        public int Strength { get; set; }
        
        public int Defense { get; set; }
        
        public int Intelligence { get; set; }
        
        public string Class { get; set; }

        public WeaponResponse Weapon { get; set; }
    }
}