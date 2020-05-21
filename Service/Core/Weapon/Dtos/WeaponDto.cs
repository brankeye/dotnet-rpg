using System;

namespace dotnet_rpg.Service.Core.Weapon.Dtos
{
    public class WeaponDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}
