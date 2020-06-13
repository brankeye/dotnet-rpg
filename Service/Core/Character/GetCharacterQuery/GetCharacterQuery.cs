using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Core.Character.GetCharacterQuery
{
    public class GetCharacterQuery : IQuery<GetCharacterQueryResult>, IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
        
        public Guid Id { get; set; }
    }
}