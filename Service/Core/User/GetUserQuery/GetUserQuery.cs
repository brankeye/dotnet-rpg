using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Core.User.GetUserQuery
{
    public class GetUserQuery : IQuery<GetUserQueryResult>, IAuthorizedBehavior
    {
        public Guid UserId { get; set; }
    }
}