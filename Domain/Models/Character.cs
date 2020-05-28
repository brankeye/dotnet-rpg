using System;
using dotnet_rpg.Domain.Enums;

namespace dotnet_rpg.Domain.Models
{
    public class Character
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int HitPoints { get; set; }

        public int Strength { get; set; }

        public int Defense { get; set; }

        public int Intelligence { get; set; }

        public RpgClass Class { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid? WeaponId { get; set; }
        
        public Weapon Weapon { get; set; }
    }
}