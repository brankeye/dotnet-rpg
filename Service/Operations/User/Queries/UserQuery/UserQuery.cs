using System;
using dotnet_rpg.Service.Contracts.CQRS.Query;

namespace dotnet_rpg.Service.Operations.User.Queries.UserQuery
{
    public class UserQuery : IAuthorizedQuery<UserQueryResult>
    {
        public Guid UserId { get; set; }
    }
}