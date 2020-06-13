using System;

namespace dotnet_rpg.Service.Behaviors.Authorized
{
    public interface IAuthorizedBehavior : IBehavior
    {
        Guid UserId { get; set; }
    }
}