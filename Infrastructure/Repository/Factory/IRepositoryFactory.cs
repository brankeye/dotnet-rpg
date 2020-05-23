using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.CharacterWeapon;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;

namespace dotnet_rpg.Infrastructure.Repository.Factory
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository();
        
        ICharacterRepository GetCharacterRepository();
        
        IWeaponRepository GetWeaponRepository();
        
        ICharacterWeaponRepository GetCharacterWeaponRepository();
    }
    
    public interface IRepositoryFactory<out T> where T : class
    {
        T GetRepository();
    }
}