using System;

namespace dotnet_rpg.Domain.Models
{
    public class CharacterWeapon
    {
        public Guid CharacterId { get; set; }
        
        public Character Character { get; set; }
        
        public Guid WeaponId { get; set; }
        
        public Weapon Weapon { get; set; }
    }
}