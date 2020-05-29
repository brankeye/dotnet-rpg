using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Api.Services.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Mapper;
using dotnet_rpg.Service.Core.Character.Validator;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Core.Character
{
    public class CharacterService : ICharacterService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICharacterValidator _characterValidator;
        private readonly ICharacterMapper _characterMapper;

        public CharacterService(
            IServiceContext serviceContext,
            IUnitOfWork unitOfWork,
            ICharacterValidator characterValidator) 
        {
            _serviceContext = serviceContext;
            _unitOfWork = unitOfWork;
            _characterValidator = characterValidator;
            _characterMapper = new CharacterMapper();
        }

        public async Task<IEnumerable<CharacterDto>> GetAllAsync()
        {
            var characters = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .ToListAsync();
            return characters.Select(_characterMapper.Map);
        }

        public async Task<CharacterDto> GetByIdAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            return _characterMapper.Map(character);
        }

        public async Task<CharacterDto> CreateAsync(CreateCharacterDto dto) 
        {
            _characterValidator.ValidateAndThrow(dto);
            var newCharacter = _characterMapper.Map(dto, _serviceContext.UserId);
            var character = _unitOfWork.Characters.Create(newCharacter);
            await _unitOfWork.CommitAsync();
            return _characterMapper.Map(character);
        }

        public async Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto) 
        {
            _characterValidator.ValidateAndThrow(dto);
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            UpdateModel(character, dto);
            _unitOfWork.Characters.Update(character);
            await _unitOfWork.CommitAsync();
            return _characterMapper.Map(character);
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

            return _characterMapper.Map(character);
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
            
            return _characterMapper.Map(character);
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
    }
}