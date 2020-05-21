using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repositories.WeaponRepository
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly DataContext _dataContext;

        public WeaponRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Weapon>> GetAllAsync(Guid userId)
        {
            var weapons = await _dataContext.Weapons
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return weapons;
        }

        public async Task<Weapon> GetByIdAsync(Guid userId, Guid id)
        {
            var weapon = await _dataContext.Weapons
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (weapon == null)
            {
                throw new NotFoundException(typeof(CharacterWeapon), id);
            }

            return weapon;
        }

        public async Task<Weapon> CreateAsync(Guid userId, Weapon newWeapon)
        {
            if (newWeapon == null)
            {
                throw new ArgumentNullException(nameof(newWeapon));
            }

            newWeapon.Id = Guid.NewGuid();
            newWeapon.UserId = userId;

            await _dataContext.Weapons.AddAsync(newWeapon);
            await _dataContext.SaveChangesAsync();

            return newWeapon;
        }

        public async Task<Weapon> UpdateAsync(Guid userId, Guid id, Weapon newWeapon)
        {
            if (newWeapon == null)
            {
                throw new ArgumentNullException(nameof(newWeapon));
            }

            var weapon = await _dataContext.Weapons
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (weapon == null)
            {
                throw new NotFoundException(typeof(CharacterWeapon), id);
            }

            Update(weapon, newWeapon);
            _dataContext.Weapons.Update(weapon);

            return weapon;
        }

        public async Task<Weapon> DeleteAsync(Guid userId, Guid id)
        {
            var weapon = await _dataContext.Weapons
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Id == id);

            if (weapon == null)
            {
                throw new NotFoundException(typeof(Weapon), id);
            }

            _dataContext.Weapons.Remove(weapon);

            return weapon;
        }

        private static void Update(Weapon dbWeapon, Weapon newWeapon)
        {
            if (newWeapon == null)
            {
                throw new ArgumentNullException(nameof(newWeapon));
            }

            dbWeapon.Name = newWeapon.Name;
            dbWeapon.Damage = newWeapon.Damage;
        }
    }
}