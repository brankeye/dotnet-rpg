using System;

namespace dotnet_rpg.Service.Operations.Auth
{
    public interface IAuthContext
    {
        Guid UserId { get; }
        
        string Username { get; }
    }
}