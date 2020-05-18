using System;

namespace dotnet_rpg.Api.Context
{
    public interface IApplicationContext
    {
        Guid UserId { get; }
    }
}