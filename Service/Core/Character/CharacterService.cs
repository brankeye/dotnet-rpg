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

        public CharacterService(
            IServiceContext serviceContext,
            ICharacterValidator characterValidator,
            IUnitOfWork unitOfWork) 
        {
            _serviceContext = serviceContext;
            _characterValidator = characterValidator;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<CharacterDto>> GetAllAsync()
        {
            var characters = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .ToListAsync();
            var dtos = characters.Select(ToDto).ToList();
            return dtos;
        }

        public async Task<CharacterDto> GetByIdAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
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
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            UpdateModel(character, dto);
            _unitOfWork.Characters.Update(character);
            await _unitOfWork.CommitAsync();
            return ToDto(character);
        }

        public async Task DeleteAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            _unitOfWork.Characters.Delete(character);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == weaponId)
                .SingleAsync();

            character.WeaponId = weapon.Id;
            character.Weapon = weapon;
            _unitOfWork.Characters.Update(character);
            await _unitOfWork.CommitAsync();

            return ToDto(character);
        }
        
        public async Task<CharacterDto> UnequipWeaponAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            if (character.WeaponId == null)
            {
                throw new ServiceException("Character has not equipped a weapon");
            }

            character.WeaponId = null;
            character.Weapon = null;
            _unitOfWork.Characters.Update(character);
            await _unitOfWork.CommitAsync();
            
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

        private static void UpdateModel(Domain.Models.Character character, UpdateCharacterDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            character.Name = dto.Name;
            character.Class = (RpgClass) Enum.Parse(typeof(RpgClass), dto.Class);
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
                Weapon = character.Weapon == null ? null : new WeaponDto
                {
                    Id = character.Weapon.Id,
                    Name = character.Weapon.Name,
                    Damage = character.Weapon.Damage,
                }
            };
        }
    }
}