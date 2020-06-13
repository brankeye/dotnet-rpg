using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Behaviors.GeneratedId;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Weapon.CreateWeaponCommand
{
    public class CreateWeaponCommand : 
        ICommand, 
        IAuthorizedBehavior, 
        IGeneratedIdBehavior
    {
        public string Name { get; set; }

        public int Damage { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid GeneratedId { get; set; }
    }
}