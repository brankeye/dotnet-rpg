using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Behaviors.GeneratedId;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.CreateCharacterCommand
{
    public class CreateCharacterCommand : 
        ICommand, 
        IAuthorizedBehavior, 
        IGeneratedIdBehavior
    {
        public string Name { get; set; }

        public string Class { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid GeneratedId { get; set; }
    }
}