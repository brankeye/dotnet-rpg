using System;

namespace dotnet_rpg.Service.Core.Weapon.GetWeaponQuery
{
    public class GetWeaponQueryResult
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int Damage { get; set; }
    }
}