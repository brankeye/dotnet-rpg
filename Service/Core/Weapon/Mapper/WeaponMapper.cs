using System;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Service.Core.Weapon.Mapper
{
    public class WeaponMapper : IWeaponMapper
    {
        public WeaponDto Map(Domain.Models.Weapon source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new WeaponDto
            {
                Id = source.Id,
                Name = source.Name,
                Damage = source.Damage
            };
        }

        public Domain.Models.Weapon Map(CreateWeaponDto source, Guid userId)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Domain.Models.Weapon
            {
                UserId = userId,
                Name = source.Name,
                Damage = source.Damage
            };
        }

        public Domain.Models.Weapon Map(UpdateWeaponDto source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Domain.Models.Weapon
            {
                Name = source.Name,
                Damage = source.Damage
            };
        }
    }
}