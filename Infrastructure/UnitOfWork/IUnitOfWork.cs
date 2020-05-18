using System;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.Repositories.CharacterRepository;
using dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository;
using dotnet_rpg.Infrastructure.Repositories.UserRepository;
using dotnet_rpg.Infrastructure.Repositories.WeaponRepository;

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