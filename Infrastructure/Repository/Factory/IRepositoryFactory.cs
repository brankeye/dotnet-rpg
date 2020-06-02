using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.Skill;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;

namespace dotnet_rpg.Infrastructure.Repository.Factory
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository();
        
        ICharacterRepository GetCharacterRepository();
        
        IWeaponRepository GetWeaponRepository();

        ISkillRepository GetSkillRepository();
    }
    
    public interface IRepositoryFactory<out T> where T : class
    {
        T GetRepository();
    }
}