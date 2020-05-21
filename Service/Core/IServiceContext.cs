using System;

namespace dotnet_rpg.Service.Core
{
    public interface IServiceContext
    {
        Guid UserId { get; }
        
        string Username { get; }
    }
}