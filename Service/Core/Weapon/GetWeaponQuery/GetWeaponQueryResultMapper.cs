using System;
using dotnet_rpg.Service.Contracts.Mapping;

namespace dotnet_rpg.Service.Core.Weapon.GetWeaponQuery
{
    public class GetWeaponQueryResultMapper : IMapper<Domain.Models.Weapon, GetWeaponQueryResult>
    {
        public GetWeaponQueryResult Map(Domain.Models.Weapon input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return new GetWeaponQueryResult
            {
                Id = input.Id,
                Name = input.Name,
                Damage = input.Damage
            };
        }
    }
}
