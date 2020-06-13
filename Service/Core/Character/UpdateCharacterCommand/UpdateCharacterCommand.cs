using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.UpdateCharacterCommand
{
    public class UpdateCharacterCommand : 
        ICommand, 
        IAuthorizedBehavior
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string Class { get; set; }
        
        public Guid UserId { get; set; }
    }
}