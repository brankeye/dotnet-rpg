using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Core.Weapon.Mapper;
using dotnet_rpg.Service.Core.Weapon.Validator;
using dotnet_rpg.Service.Operations.Auth;

namespace dotnet_rpg.Service.Core.Weapon
{
    public class WeaponService : IWeaponService
    {
        private readonly IAuthContext _authContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWeaponValidator _weaponValidator;
        private readonly IWeaponMapper _weaponMapper;

        public WeaponService(
            IAuthContext authContext, 
            IUnitOfWork unitOfWork,
            IWeaponValidator weaponValidator) 
        {
            _authContext = authContext;
            _unitOfWork = unitOfWork;
            _weaponValidator = weaponValidator;
            _weaponMapper = new WeaponMapper();
        }

        public async Task<IEnumerable<WeaponDto>> GetAllAsync() 
        {
            var weapons = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _authContext.UserId)
                .ToListAsync();
            return weapons.Select(_weaponMapper.Map);
        }

        public async Task<WeaponDto> GetByIdAsync(Guid id)
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            return _weaponMapper.Map(weapon);
        }

        public async Task<WeaponDto> CreateAsync(CreateWeaponDto dto) 
        {
            _weaponValidator.ValidateAndThrow(dto);
            var newWeapon = _weaponMapper.Map(dto, _authContext.UserId);
            var weapon = _unitOfWork.Weapons.Create(newWeapon);
            await _unitOfWork.CommitAsync();
            return _weaponMapper.Map(weapon);
        }

        public async Task<WeaponDto> UpdateAsync(Guid id, UpdateWeaponDto dto) 
        {
            _weaponValidator.ValidateAndThrow(dto);
            
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            UpdateModel(weapon, dto);
            _unitOfWork.Weapons.Update(weapon);
            await _unitOfWork.CommitAsync();
            
            return _weaponMapper.Map(weapon);
        }

        public async Task DeleteAsync(Guid id) 
        {
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _authContext.UserId)
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
    }
}