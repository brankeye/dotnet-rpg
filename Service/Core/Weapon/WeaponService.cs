using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Validator;

namespace dotnet_rpg.Service.Core.Weapon
{
    public class WeaponService : IWeaponService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeaponValidator _weaponValidator;

        public WeaponService(
            IServiceContext serviceContext, 
            IWeaponValidator weaponValidator, 
            IUnitOfWork unitOfWork) 
        {
            _serviceContext = serviceContext;
            _weaponValidator = weaponValidator;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<WeaponDto>> GetAllAsync() 
        {
            var weapons = await _unitOfWork.Weapons
                .GetAllAsync(x => x.UserId ==_serviceContext.UserId);
            var dtos = weapons.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<WeaponDto> GetByIdAsync(Guid id)
        {
            var weapon = await _unitOfWork.Weapons
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            return ToDto(weapon);
        }

        public async Task<WeaponDto> CreateAsync(CreateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            var weapon = _unitOfWork.Weapons.Create(ToModel(dto));
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> UpdateAsync(Guid id, UpdateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            
            var existingWeapon = await _unitOfWork.Weapons
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);

            existingWeapon.Name = dto.Name;
            existingWeapon.Damage = dto.Damage;
            
            var weapon = _unitOfWork.Weapons.Update(existingWeapon);
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> DeleteAsync(Guid id) 
        {
            var existingWeapon = await _unitOfWork.Weapons
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            
            _unitOfWork.Weapons.Delete(existingWeapon);
            
            await _unitOfWork.CommitAsync();
            
            return ToDto(existingWeapon);
        }

        private static Domain.Models.Weapon ToModel(CreateWeaponDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Weapon
            {
                Name = dto.Name,
                Damage = dto.Damage
            };
        }

        private static Domain.Models.Weapon ToModel(Guid userId, UpdateWeaponDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Weapon
            {
                UserId = userId,
                Name = dto.Name,
                Damage = dto.Damage
            };
        }

        private static WeaponDto ToDto(Domain.Models.Weapon weapon)
        {
            if (weapon == null)
            {
                throw new ArgumentNullException(nameof(weapon));
            }

            return new WeaponDto
            {
                Id = weapon.Id,
                Name = weapon.Name,
                Damage = weapon.Damage
            };
        }
    }
}