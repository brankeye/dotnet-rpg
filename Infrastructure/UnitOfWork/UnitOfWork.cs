using System;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.Extensions;
using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.Skill;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;
using dotnet_rpg.Infrastructure.Repository.Factory;
using dotnet_rpg.Infrastructure.Repository.Persister;

namespace dotnet_rpg.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IRepositoryPersister _repositoryPersister;

        public UnitOfWork(IRepositoryFactory repositoryFactory, IRepositoryPersister repositoryPersister)
        {
            _repositoryFactory = repositoryFactory;
            _repositoryPersister = repositoryPersister;
        }

        public IUserRepository Users => _repositoryFactory.GetUserRepository();

        public ICharacterRepository Characters => _repositoryFactory.GetCharacterRepository();

        public IWeaponRepository Weapons => _repositoryFactory.GetWeaponRepository();
        
        public ISkillRepository Skills => _repositoryFactory.GetSkillRepository();

        public void Commit()
        {
            try
            {
                _repositoryPersister.Commit();
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
                await _repositoryPersister.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException("Failed to commit changes");
            }
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositoryPersister.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}