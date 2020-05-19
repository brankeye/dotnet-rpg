using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Repositories.CharacterRepository;
using dotnet_rpg.Infrastructure.Repositories.UserRepository;
using dotnet_rpg.Infrastructure.Repositories.WeaponRepository;
using dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository;

namespace dotnet_rpg.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        private IUserRepository _users;
        public IUserRepository Users => _users ??= new UserRepository(_dataContext);

        private ICharacterRepository _characters;
        public ICharacterRepository Characters => _characters ??= new CharacterRepository(_dataContext);

        private IWeaponRepository _weapons;
        public IWeaponRepository Weapons => _weapons ??= new WeaponRepository(_dataContext);
        
        private ICharacterWeaponRepository _characterWeapons;
        public ICharacterWeaponRepository CharacterWeapons => _characterWeapons ??= new CharacterWeaponRepository(_dataContext);

        public void Commit()
        {
            _dataContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}