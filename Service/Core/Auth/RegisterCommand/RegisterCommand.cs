using System;
using dotnet_rpg.Service.Behaviors.GeneratedId;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Auth.RegisterCommand
{
    public class RegisterCommand : ICommand, IGeneratedIdBehavior
    {
        public Guid GeneratedId { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
    }
}