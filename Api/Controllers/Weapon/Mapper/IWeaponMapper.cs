using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Api.Mapper;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Api.Controllers.Weapon.Mapper
{
    public interface IWeaponMapper : 
        IMapper<WeaponDto, WeaponResponse>,
        IMapper<CreateWeaponRequest, CreateWeaponDto>,
        IMapper<UpdateWeaponRequest, UpdateWeaponDto>
    {
        
    }
}