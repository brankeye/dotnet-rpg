using System;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repositories.CharacterWeaponRepository
{
    public class CharacterWeaponRepository : ICharacterWeaponRepository
    {
        private readonly DataContext _dataContext;
        
        public CharacterWeaponRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<CharacterWeapon> GetByIdAsync(Guid characterId, Guid weaponId)
        {
            var characterWeapon = await _dataContext.CharacterWeapons
                .SingleOrDefaultAsync(x => x.CharacterId == characterId && x.WeaponId == weaponId);

            if (characterWeapon == null)
            {
                throw new NotFoundException(
                    nameof(CharacterWeapon.CharacterId), 
                    characterId,
                    nameof(CharacterWeapon.WeaponId),
                    weaponId);
            }

            return characterWeapon;
        }
        
        public async Task<CharacterWeapon> GetByCharacterIdAsync(Guid characterId)
        {
            var characterWeapon = await _dataContext.CharacterWeapons
                .SingleOrDefaultAsync(x => x.CharacterId == characterId);

            if (characterWeapon == null)
            {
                throw new NotFoundException(nameof(CharacterWeapon.CharacterId), characterId);
            }

            return characterWeapon;
        }
        
        public async Task<CharacterWeapon> GetByWeaponIdAsync(Guid weaponId)
        {
            var characterWeapon = await _dataContext.CharacterWeapons
                .SingleOrDefaultAsync(x => x.WeaponId == weaponId);

            if (characterWeapon == null)
            {
                throw new NotFoundException(nameof(CharacterWeapon.WeaponId), weaponId);
            }

            return characterWeapon;
        }

        public async Task<CharacterWeapon> CreateAsync(CharacterWeapon newCharacterWeapon)
        {
            if (newCharacterWeapon == null)
            {
                throw new ArgumentNullException(nameof(newCharacterWeapon));
            }

            var character = await _dataContext.CharacterWeapons.AddAsync(newCharacterWeapon);

            return character.Entity;
        }
        
        public async Task<CharacterWeapon> UpdateAsync(CharacterWeapon newCharacterWeapon)
        {
            if (newCharacterWeapon == null)
            {
                throw new ArgumentNullException(nameof(newCharacterWeapon));
            }

            var dbCharacterWeapon = await _dataContext.CharacterWeapons
                .FirstOrDefaultAsync(x => x.CharacterId == newCharacterWeapon.CharacterId && x.WeaponId == newCharacterWeapon.WeaponId);

            if (dbCharacterWeapon == null)
            {
                throw new NotFoundException(
                    nameof(CharacterWeapon.CharacterId), 
                    newCharacterWeapon.CharacterId,
                    nameof(CharacterWeapon.WeaponId), 
                    newCharacterWeapon.WeaponId);
            }

            dbCharacterWeapon.CharacterId = newCharacterWeapon.CharacterId;
            dbCharacterWeapon.WeaponId = newCharacterWeapon.WeaponId;
            
            var result = _dataContext.CharacterWeapons.Update(dbCharacterWeapon);

            return result.Entity;
        }

        public async Task<CharacterWeapon> DeleteByCharacterIdAsync(Guid characterId)
        {
            var characterWeapon = await _dataContext.CharacterWeapons
                .SingleOrDefaultAsync(x => x.CharacterId == characterId);

            if (characterWeapon == null) 
            {
                throw new NotFoundException();
            }

            _dataContext.CharacterWeapons.Remove(characterWeapon);

            return characterWeapon;
        }
    }
}