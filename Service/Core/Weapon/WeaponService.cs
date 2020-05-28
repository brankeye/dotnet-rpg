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
            var weapons = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .ToListAsync();
            var dtos = weapons.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<WeaponDto> GetByIdAsync(Guid id)
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> CreateAsync(CreateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            var weapon = _unitOfWork.Weapons.Create(ToModel(_serviceContext.UserId, dto));
            await _unitOfWork.CommitAsync();
            return ToDto(weapon);
        }

        public async Task<WeaponDto> UpdateAsync(Guid id, UpdateWeaponDto dto) 
        {
            _weaponValidator.Validate(dto);
            
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            UpdateModel(weapon, dto);
            _unitOfWork.Weapons.Update(weapon);
            await _unitOfWork.CommitAsync();
            
            return ToDto(weapon);
        }

        public async Task DeleteAsync(Guid id) 
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            
            _unitOfWork.Weapons.Delete(weapon);
            
            await _unitOfWork.CommitAsync();
        }
        
        private static void UpdateModel(Domain.Models.Weapon weapon, UpdateWeaponDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            weapon.Name = dto.Name;
            weapon.Damage = dto.Damage;
        }

        private static Domain.Models.Weapon ToModel(Guid userId, CreateWeaponDto dto)
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