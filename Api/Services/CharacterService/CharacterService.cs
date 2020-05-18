using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Api.Validators;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Api.Context;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Api.Dtos.Character;
using dotnet_rpg.Api.Dtos.Weapon;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Api.Validators.CharacterValidator;
using dotnet_rpg.Domain.Models;

namespace dotnet_rpg.Api.Services.CharacterService
{
    public class CharacterService : Services.CharacterService.ICharacterService
    {
        private readonly IApplicationContext _appContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICharacterValidator _characterValidator;

        public CharacterService(IApplicationContext appContext, IUnitOfWork unitOfWork) 
        {
            _appContext = appContext;
            _unitOfWork = unitOfWork;
            _characterValidator = new CharacterValidator();
        }

        public async Task<IList<CharacterDto>> GetAllAsync() 
        {
            var characters = await _unitOfWork.Characters.GetAllAsync(_appContext.UserId);
            var dtos = characters.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<CharacterDto> GetByIdAsync(Guid id) 
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(_appContext.UserId, id);
            return ToDto(character);
        }

        public async Task<CharacterDto> CreateAsync(CreateCharacterDto dto) 
        {
            _characterValidator.Validate(dto);
            var character = await _unitOfWork.Characters.CreateAsync(_appContext.UserId, ToModel(dto));
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto) 
        {
            _characterValidator.Validate(dto);
            var character = await _unitOfWork.Characters.UpdateAsync(_appContext.UserId, id, ToModel(dto));
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> DeleteAsync(Guid id) 
        {
            var character = await _unitOfWork.Characters.DeleteAsync(_appContext.UserId, id);
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(_appContext.UserId, id);
            var weapon = await _unitOfWork.Weapons.GetByIdAsync(_appContext.UserId, weaponId);

            CharacterWeapon currentCharacterWeapon = null;
            try
            {
                currentCharacterWeapon = await _unitOfWork.CharacterWeapons.GetByWeaponIdAsync(weapon.Id);
            }
            catch (Exception)
            {
                // ignored
            }

            if (currentCharacterWeapon != null)
            {
                if (currentCharacterWeapon.CharacterId != character.Id)
                {
                    throw new ValidationException("Weapon already equipped on another character");
                } else if (currentCharacterWeapon.WeaponId == weapon.Id)
                {
                    throw new ValidationException("Weapon already equipped on this character");
                }

                currentCharacterWeapon.WeaponId = weapon.Id;
                await _unitOfWork.CharacterWeapons.UpdateAsync(currentCharacterWeapon);
            }
            else
            {
                var newCharacterWeapon = new CharacterWeapon
                {
                    CharacterId = character.Id,
                    WeaponId = weapon.Id
                };

                await _unitOfWork.CharacterWeapons.CreateAsync(newCharacterWeapon);
            }
            
            await _unitOfWork.CommitAsync();

            return ToDto(character);
        }
        
        public async Task<CharacterDto> UnequipWeaponAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.GetByIdAsync(_appContext.UserId, id);

            try
            {
                await _unitOfWork.CharacterWeapons.DeleteByCharacterIdAsync(character.Id);
            }
            catch (NotFoundException)
            {
                throw new ValidationException("Character has not equipped a weapon");
            }
            
            await _unitOfWork.CommitAsync();

            character.CharacterWeapon = null;
            return ToDto(character);
        }

        private static Domain.Models.Character ToModel(CreateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Character
            {
                Name = dto.Name,
                HitPoints = 100,
                Strength = 1,
                Defense = 1,
                Intelligence = 1,
                Class = (RpgClass)Enum.Parse(typeof(RpgClass), dto.Class)
            };
        }

        private static Domain.Models.Character ToModel(UpdateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Character
            {
                Name = dto.Name,
                HitPoints = 100,
                Strength = 1,
                Defense = 1,
                Intelligence = 1,
                Class = (RpgClass)Enum.Parse(typeof(RpgClass), dto.Class)
            };
        }

        private static CharacterDto ToDto(Domain.Models.Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException(nameof(character));
            }

            return new CharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                HitPoints = character.HitPoints,
                Strength = character.Strength,
                Defense = character.Defense,
                Intelligence = character.Intelligence,
                Class = character.Class.ToString(),
                Weapon = character.CharacterWeapon?.Weapon == null ? null : new WeaponDto
                {
                    Id = character.CharacterWeapon.Weapon.Id,
                    Name = character.CharacterWeapon.Weapon.Name,
                    Damage = character.CharacterWeapon.Weapon.Damage,
                }
            };
        }
    }
}