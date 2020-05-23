using System;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.CharacterWeapon;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;

namespace dotnet_rpg.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        ICharacterRepository Characters { get; }

        IWeaponRepository Weapons { get; }
        
        ICharacterWeaponRepository CharacterWeapons { get; }

        void Commit();

        Task CommitAsync();
    }
}