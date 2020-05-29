using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Api.Controllers.Weapon.Mapper
{
    public interface IWeaponMapper
    {
        WeaponResponse Map(WeaponDto dto);

        CreateWeaponDto Map(CreateWeaponRequest request);
        
        UpdateWeaponDto Map(UpdateWeaponRequest request);
    }
}