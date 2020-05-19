using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using dotnet_rpg.Domain.Models;

namespace dotnet_rpg.Infrastructure.Repositories.CharacterRepository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _dataContext;

        public CharacterRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Domain.Models.Character>> GetAllAsync(Guid userId)
        {
            var characters = await _dataContext.Characters
                .Where(x => x.UserId == userId)
                .Include(x => x.CharacterWeapon)
                .ThenInclude(x => x.Weapon)
                .ToListAsync();
            return characters;
        }

        public async Task<Domain.Models.Character> GetByIdAsync(Guid userId, Guid id)
        {
            var character = await _dataContext.Characters
                .Include(x => x.CharacterWeapon)
                .ThenInclude(x => x.Weapon)
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (character == null)
            {
                throw new NotFoundException(id);
            }

            return character;
        }

        public async Task<Domain.Models.Character> CreateAsync(Guid userId, Domain.Models.Character newCharacter)
        {
            if (newCharacter == null)
            {
                throw new ArgumentNullException(nameof(newCharacter));
            }

            newCharacter.Id = Guid.NewGuid();
            newCharacter.UserId = userId;

            await _dataContext.Characters.AddAsync(newCharacter);

            var character = await _dataContext.Characters
                .Include(x => x.CharacterWeapon)
                .ThenInclude(x => x.Weapon)
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == newCharacter.Id);

            return character;
        }

        public async Task<Domain.Models.Character> UpdateAsync(Guid userId, Guid id, Domain.Models.Character newCharacter) 
        {
            if (newCharacter == null)
            {
                throw new ArgumentNullException(nameof(newCharacter));
            }

            var character = await _dataContext.Characters
                .Include(x => x.CharacterWeapon)
                .ThenInclude(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (character == null)
            {
                throw new NotFoundException(id);
            }
            
            Update(character, newCharacter);
            _dataContext.Characters.Update(character);

            return character;
        }

        public async Task<Domain.Models.Character> DeleteAsync(Guid userId, Guid id) 
        {
            var character = await _dataContext.Characters
                .Include(x => x.CharacterWeapon)
                .ThenInclude(x => x.Weapon)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (character == null) 
            {
                throw new NotFoundException(id);
            }

            _dataContext.Characters.Remove(character);

            return character;
        }

        private static void Update(Character dbCharacter, Character newCharacter)
        {
            if (newCharacter == null)
            {
                throw new ArgumentNullException(nameof(newCharacter));
            }

            dbCharacter.Name = newCharacter.Name;
            dbCharacter.Class = newCharacter.Class;
        }
    }
}