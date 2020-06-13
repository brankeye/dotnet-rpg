using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;

namespace dotnet_rpg.Service.Core.Weapon.GetWeaponQuery
{
    public class GetWeaponQuery : IQuery<GetWeaponQueryResult>, IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
        
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }
    }
}