using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Api.Context;
using dotnet_rpg.Api.Dtos.Weapon;
using dotnet_rpg.Api.Validators.WeaponValidator;

namespace dotnet_rpg.Api.Services.WeaponService
{
    public class WeaponService : Services.WeaponService.IWeaponService
    {
        private readonly IApplicationContext _appContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeaponValidator _weaponValidator;

        public WeaponService(IApplicationContext appContext, IUnitOfWork unitOfWork) 
        {
            _appContext = appContext;
            _unitOfWork = unitOfWork;
            _weaponValidator = new WeaponValidator();
        }

        public async Task<IList<WeaponDto>> GetAllAsync() 
        {
            var weapons = await _unitOfWork.Weapons.GetAllAsync(_appContext.UserId);
            var dtos = weapons.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<WeaponDto> GetByIdAsync(Guid id)
        {
            var weapon = await _unitOfWork.Weapons.GetByIdAsync(_appContext.UserId, id);
            return ToDto(weapon);
        }

        public async Task<WeaponDto> CreateAsync(CreateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            var weapon = await _unitOfWork.Weapons.CreateAsync(_appContext.UserId, ToModel(dto));
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> UpdateAsync(Guid id, UpdateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            var weapon = await _unitOfWork.Weapons.UpdateAsync(_appContext.UserId, id, ToModel(dto));
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> DeleteAsync(Guid id) 
        {
            var weapon = await _unitOfWork.Weapons.DeleteAsync(_appContext.UserId, id);
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
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

        private static Domain.Models.Weapon ToModel(UpdateWeaponDto dto)
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