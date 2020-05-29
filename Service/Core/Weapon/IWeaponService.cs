using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Weapon.Dtos;

namespace dotnet_rpg.Service.Core.Weapon
{
    public interface IWeaponService
    {
        Task<IEnumerable<WeaponDto>> GetAllAsync();
        
        Task<WeaponDto> GetByIdAsync(Guid id);
        
        Task<WeaponDto> CreateAsync(CreateWeaponDto dto);
        
        Task<WeaponDto> UpdateAsync(Guid id, UpdateWeaponDto dto);
        
        Task DeleteAsync(Guid id);
    }
}