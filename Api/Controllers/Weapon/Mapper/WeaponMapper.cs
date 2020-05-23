using dotnet_rpg.Api.Controllers.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Api.Controllers.Weapon.Mapper
{
    public class WeaponMapper : IWeaponMapper
    {
        public WeaponResponse Map(WeaponDto source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new WeaponResponse
            {
                Id = source.Id,
                Name = source.Name,
                Damage = source.Damage
            };
        }

        public CreateWeaponDto Map(CreateWeaponRequest source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new CreateWeaponDto
            {
                Name = source.Name,
                Damage = source.Damage
            };
        }

        public UpdateWeaponDto Map(UpdateWeaponRequest source)
        {
            if (source == null)
            {
                return null;
            }
            
            return new UpdateWeaponDto
            {
                Name = source.Name,
                Damage = source.Damage
            };
        }
    }
}