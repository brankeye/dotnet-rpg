using System;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Weapon.CreateWeaponCommand
{
    public class CreateWeaponCommandMapper : IMapper<CreateWeaponCommand, Domain.Models.Weapon>
    {
        public Domain.Models.Weapon Map(CreateWeaponCommand input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new Domain.Models.Weapon
            {
                UserId = input.UserId,
                Id = input.GeneratedId,
                Name = input.Name,
                Damage = input.Damage
            };
        }
    }
}