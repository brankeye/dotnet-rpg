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
            try
            {
                var characters = await _dataContext.Characters
                    .Where(x => x.UserId == userId)
                    .Include(x => x.CharacterWeapon)
                    .ThenInclude(x => x.Weapon)
                    .ToListAsync();
                return characters;
            }
            catch (Exception ex) when (!(ex is RepositoryException))
            {
                throw new RepositoryException("Failed to get characters", ex);
            }
        }

        public async Task<Domain.Models.Character> GetByIdAsync(Guid userId, Guid id)
        {
            try
            {
                var character = await _dataContext.Characters
                    .Include(x => x.CharacterWeapon)
                    .ThenInclude(x => x.Weapon)
                    .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                if (character == null)
                {
                    throw new NotFoundException(typeof(Character), id);
                }

                return character;
            }
            catch (Exception ex) when (!(ex is RepositoryException))
            {
                throw new RepositoryException("Failed to get character", ex);
            }
        }

        public async Task<Domain.Models.Character> CreateAsync(Guid userId, Domain.Models.Character newCharacter)
        {
            try
            {
                newCharacter.Id = Guid.NewGuid();
                newCharacter.UserId = userId;

                var entry = await _dataContext.Characters.AddAsync(newCharacter);

                return entry.Entity;
            }
            catch (Exception ex) when (!(ex is RepositoryException))
            {
                throw new RepositoryException("Failed to create character", ex);
            }
        }

        public async Task<Domain.Models.Character> UpdateAsync(Guid userId, Guid id, Domain.Models.Character newCharacter) 
        {
            try
            {
                var character = await _dataContext.Characters
                    .Include(x => x.CharacterWeapon)
                    .ThenInclude(x => x.Weapon)
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                if (character == null)
                {
                    throw new NotFoundException(id);
                }
            
                Update(character, newCharacter);
                var entry = _dataContext.Characters.Update(character);

                return entry.Entity;
            }
            catch (Exception ex) when (!(ex is RepositoryException))
            {
                throw new RepositoryException("Failed to update character", ex);
            }
        }

        public async Task<Domain.Models.Character> DeleteAsync(Guid userId, Guid id) 
        {
            try
            {
                var character = await _dataContext.Characters
                    .Include(x => x.CharacterWeapon)
                    .ThenInclude(x => x.Weapon)
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

                if (character == null)
                {
                    throw new NotFoundException(typeof(Character), id);
                }

                var entry = _dataContext.Characters.Remove(character);

                return entry.Entity;
            }
            catch (Exception ex) when (!(ex is RepositoryException))
            {
                throw new RepositoryException("Failed to delete character", ex);
            }
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