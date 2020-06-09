using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Character.Dtos;

namespace dotnet_rpg.Service.Core.Character
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterDto>> GetAllAsync();
        
        Task<CharacterDto> GetByIdAsync(Guid id);
        
        Task<CharacterDto> CreateAsync(CreateCharacterDto dto);
        
        Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto);
        
        Task DeleteAsync(Guid id);
        
        Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId);
        
        Task<CharacterDto> UnequipWeaponAsync(Guid id);

        Task<CharacterDto> LearnSkillAsync(Guid id, Guid skillId);
        
        Task<CharacterDto> UnlearnSkillAsync(Guid id, Guid skillId);
    }
}