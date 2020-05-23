using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.CharacterWeapon;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;

namespace dotnet_rpg.Infrastructure.Repository.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DataContext _dataContext;
        private IUserRepository _userRepository;
        private ICharacterRepository _characterRepository;
        private IWeaponRepository _weaponRepository;
        private ICharacterWeaponRepository _characterWeaponRepository;
        
        public RepositoryFactory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IUserRepository GetUserRepository()
        {
            return _userRepository ??= new UserRepository(_dataContext.Users);
        }

        public ICharacterRepository GetCharacterRepository()
        {
            return _characterRepository ??= new CharacterRepository(_dataContext.Characters);
        }

        public IWeaponRepository GetWeaponRepository()
        {
            return _weaponRepository ??= new WeaponRepository(_dataContext.Weapons);
        }

        public ICharacterWeaponRepository GetCharacterWeaponRepository()
        {
            return _characterWeaponRepository ??= new CharacterWeaponRepository(_dataContext.CharacterWeapons);
        }
    }
}