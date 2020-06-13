using System;
using dotnet_rpg.Infrastructure.Repository.Core.Character;
using dotnet_rpg.Infrastructure.Repository.Core.CharacterSkill;
using dotnet_rpg.Infrastructure.Repository.Core.Skill;
using dotnet_rpg.Infrastructure.Repository.Core.User;
using dotnet_rpg.Infrastructure.Repository.Core.Weapon;

namespace dotnet_rpg.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        ICharacterRepository Characters { get; }

        IWeaponRepository Weapons { get; }
        
        ISkillRepository Skills { get; }
        
        ICharacterSkillRepository CharacterSkills { get; }
    }
}