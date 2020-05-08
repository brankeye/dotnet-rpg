using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<IList<ReadCharacterDto>>> GetAllAsync();
        Task<ServiceResponse<ReadCharacterDto>> GetByIdAsync(Guid id);
        Task<ServiceResponse<ReadCharacterDto>> CreateAsync(WriteCharacterDto newCharacter);
        Task<ServiceResponse<ReadCharacterDto>> UpdateAsync(Guid id, WriteCharacterDto newCharacter);
        Task<ServiceResponse<ReadCharacterDto>> DeleteAsync(Guid id);
    }
}