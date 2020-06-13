using System;
using System.Collections.Generic;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;
using dotnet_rpg.Service.Core.Character.GetCharacterQuery;

namespace dotnet_rpg.Service.Core.Character.GetAllCharactersQuery
{
    public class GetAllCharactersQuery : 
        IQuery<IEnumerable<GetCharacterQueryResult>>, 
        IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
    }
}