using System;

namespace dotnet_rpg.Service.Contracts.CQRS.Query
{
    public interface IAuthorizedQuery<TResult> : IQuery<TResult>
    {
        Guid UserId { get; set; }
    }
}