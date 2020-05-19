using System;

namespace dotnet_rpg.Api.Services
{
    public interface IServiceContext
    {
        Guid UserId { get; }
    }
}