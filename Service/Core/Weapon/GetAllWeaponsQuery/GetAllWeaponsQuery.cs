using System;
using System.Collections.Generic;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Core.Weapon.GetWeaponQuery;

namespace dotnet_rpg.Service.Core.Weapon.GetAllWeaponsQuery
{
    public class GetAllWeaponsQuery : 
        IQuery<IEnumerable<GetWeaponQueryResult>>, 
        IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
    }
}