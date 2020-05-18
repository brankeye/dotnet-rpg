using System;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Models;

namespace dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository
{
    public interface ICharacterWeaponRepository
    {
        Task<CharacterWeapon> GetByIdAsync(Guid characterId, Guid weaponId);
        
        Task<CharacterWeapon> GetByCharacterIdAsync(Guid characterId);
        
        Task<CharacterWeapon> GetByWeaponIdAsync(Guid weaponId);

        Task<CharacterWeapon> CreateAsync(CharacterWeapon entity);
        
        Task<CharacterWeapon> UpdateAsync(CharacterWeapon entity);
        
        Task<CharacterWeapon> DeleteByCharacterIdAsync(Guid characterId);
    }
}