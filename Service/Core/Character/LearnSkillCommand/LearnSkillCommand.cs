using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.LearnSkillCommand
{
    public class LearnSkillCommand : 
        ICommand, 
        IAuthorizedBehavior
    {
        public Guid CharacterId { get; set; }
        
        public Guid SkillId { get; set; }
        
        public Guid UserId { get; set; }
    }
}