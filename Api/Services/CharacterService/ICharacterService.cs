using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Api.Dtos.Character;

namespace dotnet_rpg.Api.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<IList<CharacterDto>> GetAllAsync();
        Task<CharacterDto> GetByIdAsync(Guid id);
        Task<CharacterDto> CreateAsync(CreateCharacterDto dto);
        Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto);
        Task<CharacterDto> DeleteAsync(Guid id);
        Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId);
        Task<CharacterDto> UnequipWeaponAsync(Guid id);
    }
}