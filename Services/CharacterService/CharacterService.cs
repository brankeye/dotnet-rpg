using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using dotnet_rpg.Models;
using dotnet_rpg.Dtos;
using dotnet_rpg.Services;
using System;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : Services.CharacterService.ICharacterService
    {
        private readonly DataContext _context;
        private readonly CharacterValidator _characterValidator;

        public CharacterService(DataContext context) {
            _context = context;
            _characterValidator = new CharacterValidator();
        }

        public async Task<ServiceResponse<IList<ReadCharacterDto>>> GetAllAsync() {
            var characters = await _context.Characters.ToListAsync();
            var dtos = characters.Select(x => new ReadCharacterDto(x)).ToList();
            return ServiceResponse<IList<ReadCharacterDto>>.Successful(dtos);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> GetByIdAsync(Guid id) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            
            if (character == null) {
                return ServiceResponse<ReadCharacterDto>.NotSuccessful();
            }

            var dto = new ReadCharacterDto(character);
            return ServiceResponse<ReadCharacterDto>.Successful(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> CreateAsync(WriteCharacterDto createDto) {
            var character = new Character(createDto);
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            var dto = new ReadCharacterDto(character);
            return ServiceResponse<ReadCharacterDto>.Successful(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> UpdateAsync(Guid id, WriteCharacterDto updateDto) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character == null) {
                return ServiceResponse<ReadCharacterDto>.NotSuccessful();
            }

            character.Update(updateDto);
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            var dto = new ReadCharacterDto(character);
            return ServiceResponse<ReadCharacterDto>.Successful(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> DeleteAsync(Guid id) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character == null) {
                return ServiceResponse<ReadCharacterDto>.NotSuccessful();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            var dto = new ReadCharacterDto(character);
            return ServiceResponse<ReadCharacterDto>.Successful(dto);
        }
    }
}