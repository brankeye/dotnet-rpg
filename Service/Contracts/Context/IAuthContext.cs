using System;

namespace dotnet_rpg.Service.Contracts.Context
{
    public interface IAuthContext
    {
        Guid UserId { get; }
        
        string Username { get; }
    }
}