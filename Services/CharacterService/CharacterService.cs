using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using dotnet_rpg.Models;
using dotnet_rpg.Dtos;
using System;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;
using dotnet_rpg.Exceptions;
using dotnet_rpg.Validators;

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
            return new ServiceResponse<IList<ReadCharacterDto>>(dtos);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> GetByIdAsync(Guid id) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            
            if (character == null) {
                throw new NotFoundException(id);
            }

            var dto = new ReadCharacterDto(character);
            return new ServiceResponse<ReadCharacterDto>(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> CreateAsync(WriteCharacterDto createDto) {
            _characterValidator.Validate(createDto);
            var character = new Character(createDto);
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();
            var dto = new ReadCharacterDto(character);
            return new ServiceResponse<ReadCharacterDto>(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> UpdateAsync(Guid id, WriteCharacterDto updateDto) {
            _characterValidator.Validate(updateDto);
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character == null) {
                throw new NotFoundException(id);
            }

            character.Update(updateDto);
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();

            var dto = new ReadCharacterDto(character);
            return new ServiceResponse<ReadCharacterDto>(dto);
        }

        public async Task<ServiceResponse<ReadCharacterDto>> DeleteAsync(Guid id) {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character == null) {
                throw new NotFoundException(id);
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            var dto = new ReadCharacterDto(character);
            return new ServiceResponse<ReadCharacterDto>(dto);
        }
    }
}