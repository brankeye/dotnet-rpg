using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Extensions;
using dotnet_rpg.Infrastructure.Repositories.CharacterRepository;
using dotnet_rpg.Infrastructure.Repositories.UserRepository;
using dotnet_rpg.Infrastructure.Repositories.WeaponRepository;
using dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository;

namespace dotnet_rpg.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private IUserRepository _users;
        private ICharacterRepository _characters;
        private IWeaponRepository _weapons;
        private ICharacterWeaponRepository _characterWeapons;

        public UnitOfWork(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
        
        public IUserRepository Users => _users ??= new UserRepository(_dataContext);

        public ICharacterRepository Characters => _characters ??= new CharacterRepository(_dataContext);

        public IWeaponRepository Weapons => _weapons ??= new WeaponRepository(_dataContext);

        public ICharacterWeaponRepository CharacterWeapons => _characterWeapons ??= new CharacterWeaponRepository(_dataContext);

        public void Commit()
        {
            try
            {
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException("Failed to commit changes");
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException("Failed to commit changes");
            }
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}