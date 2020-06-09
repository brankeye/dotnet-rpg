using System;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Operations.Auth.Operations.RegisterCommand
{
    public class RegisterCommand : ICommand
    {
        public Guid UserId { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
    }
}