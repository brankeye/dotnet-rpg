using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.DeleteCharacterCommand
{
    public class DeleteCharacterCommand : 
        ICommand, 
        IAuthorizedBehavior
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
    }
}