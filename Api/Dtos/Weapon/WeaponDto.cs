using System;

namespace dotnet_rpg.Api.Dtos.Weapon
{
    public class WeaponDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}
