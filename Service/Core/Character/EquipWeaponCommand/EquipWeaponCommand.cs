using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Character.EquipWeaponCommand
{
    public class EquipWeaponCommand : 
        ICommand, 
        IAuthorizedBehavior
    {
        public Guid CharacterId { get; set; }
        
        public Guid WeaponId { get; set; }
        
        public Guid UserId { get; set; }
    }
}