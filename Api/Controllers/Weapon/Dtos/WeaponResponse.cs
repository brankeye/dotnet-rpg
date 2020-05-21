using System;

namespace dotnet_rpg.Api.Controllers.Weapon.Dtos
{
    public class WeaponResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}
