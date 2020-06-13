using System;
using dotnet_rpg.Service.Behaviors.Authorized;
using dotnet_rpg.Service.Contracts.CQRS.Command;

namespace dotnet_rpg.Service.Core.Weapon.UpdateWeaponCommand
{
    public class UpdateWeaponCommand : 
        ICommand, 
        IAuthorizedBehavior
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int Damage { get; set; }
        
        public Guid UserId { get; set; }
    }
}