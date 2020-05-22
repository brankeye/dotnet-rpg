using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Validator;
using dotnet_rpg.Service.Core.Weapon.Dtos;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character
{
    public class CharacterService : ICharacterService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICharacterValidator _characterValidator;

        public CharacterService(IServiceContext serviceContext, IUnitOfWork unitOfWork) 
        {
            _serviceContext = serviceContext;
            _unitOfWork = unitOfWork;
            _characterValidator = new CharacterValidator();
        }

        public async Task<IList<CharacterDto>> GetAllAsync()
        {
            var characters = await _unitOfWork.Characters
                .GetAllAsync(x => x.UserId == _serviceContext.UserId);
            var dtos = characters.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<CharacterDto> GetByIdAsync(Guid id)
        {
            var character = await _unitOfWork.Characters
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            return ToDto(character);
        }

        public async Task<CharacterDto> CreateAsync(CreateCharacterDto dto) 
        {
            _characterValidator.Validate(dto);
            var character = _unitOfWork.Characters.Create(ToModel(_serviceContext.UserId, dto));
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto) 
        {
            _characterValidator.Validate(dto);
            var character = _unitOfWork.Characters.Update(ToModel(_serviceContext.UserId, dto));
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> DeleteAsync(Guid id)
        {
            var existingCharacter = await _unitOfWork.Characters
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            var character = _unitOfWork.Characters.Delete(existingCharacter);
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId)
        {
            var character = await _unitOfWork.Characters
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            var weapon = await _unitOfWork.Weapons
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == weaponId);

            CharacterWeapon currentCharacterWeapon = null;
            try
            {
                currentCharacterWeapon = await _unitOfWork.CharacterWeapons.GetAsync(
                    x => x.WeaponId == weapon.Id);
            }
            catch (NotFoundException)
            {
                // ignored
            }

            if (currentCharacterWeapon != null)
            {
                if (currentCharacterWeapon.CharacterId != character.Id)
                {
                    throw new ServiceException("Weapon already equipped on another character");
                } else if (currentCharacterWeapon.WeaponId == weapon.Id)
                {
                    throw new ServiceException("Weapon already equipped on this character");
                }

                currentCharacterWeapon.WeaponId = weapon.Id;
                _unitOfWork.CharacterWeapons.Update(currentCharacterWeapon);
            }
            else
            {
                var newCharacterWeapon = new CharacterWeapon
                {
                    CharacterId = character.Id,
                    WeaponId = weapon.Id
                };

                _unitOfWork.CharacterWeapons.Create(newCharacterWeapon);
            }
            
            await _unitOfWork.CommitAsync();

            return ToDto(character);
        }
        
        public async Task<CharacterDto> UnequipWeaponAsync(Guid id)
        {
            var character = await _unitOfWork.Characters
                .GetAsync(x => x.UserId == _serviceContext.UserId && x.Id == id);
            var characterWeapon = await _unitOfWork.CharacterWeapons.GetAsync(x => x.CharacterId == id);
            
            try
            {
                _unitOfWork.CharacterWeapons.Delete(characterWeapon);
            }
            catch (NotFoundException)
            {
                throw new ServiceException("Character has not equipped a weapon");
            }
            
            await _unitOfWork.CommitAsync();

            character.CharacterWeapon = null;
            return ToDto(character);
        }

        private static Domain.Models.Character ToModel(Guid userId, CreateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Character
            {
                UserId = userId,
                Name = dto.Name,
                HitPoints = 100,
                Strength = 1,
                Defense = 1,
                Intelligence = 1,
                Class = (RpgClass)Enum.Parse(typeof(RpgClass), dto.Class)
            };
        }

        private static Domain.Models.Character ToModel(Guid userId, UpdateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Domain.Models.Character
            {
                UserId = userId,
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