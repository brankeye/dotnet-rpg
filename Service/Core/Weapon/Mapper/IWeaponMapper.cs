using System;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Service.Core.Weapon.Mapper
{
    public interface IWeaponMapper
    {
        WeaponDto Map(Domain.Models.Weapon weapon);

        Domain.Models.Weapon Map(CreateWeaponDto dto, Guid userId);
        
        Domain.Models.Weapon Map(UpdateWeaponDto dto);
    }
}