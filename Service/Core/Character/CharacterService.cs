using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Service.Core.Character.Dtos;
using dotnet_rpg.Service.Core.Character.Mapper;
using dotnet_rpg.Service.Core.Character.Validator;
using dotnet_rpg.Service.Exceptions;
using dotnet_rpg.Service.Operations.Auth;

namespace dotnet_rpg.Service.Core.Character
{
    public class CharacterService : ICharacterService
    {
        private readonly IAuthContext _authContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICharacterValidator _characterValidator;
        private readonly ICharacterMapper _characterMapper;

        public CharacterService(
            IAuthContext authContext,
            IUnitOfWork unitOfWork,
            ICharacterValidator characterValidator) 
        {
            _authContext = authContext;
            _unitOfWork = unitOfWork;
            _characterValidator = characterValidator;
            _characterMapper = new CharacterMapper();
        }

        public async Task<IEnumerable<CharacterDto>> GetAllAsync()
        {
            var characters = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
                .ToListAsync();
            return characters.Select(_characterMapper.Map);
        }

        public async Task<CharacterDto> GetByIdAsync(Guid id)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            return _characterMapper.Map(character);
        }

        public async Task<CharacterDto> CreateAsync(CreateCharacterDto dto) 
        {
            _characterValidator.ValidateAndThrow(dto);
            var newCharacter = _characterMapper.Map(dto, _authContext.UserId);
            var character = _unitOfWork.Characters.Create(newCharacter);
            await _unitOfWork.CommitAsync();
            return _characterMapper.Map(character);
        }

        public async Task<CharacterDto> UpdateAsync(Guid id, UpdateCharacterDto dto) 
        {
            _characterValidator.ValidateAndThrow(dto);
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
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
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            _unitOfWork.Characters.Delete(character);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CharacterDto> EquipWeaponAsync(Guid id, Guid weaponId)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            var weapon = await _unitOfWork.Weapons.Query
                .Where(x => x.UserId == _authContext.UserId)
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
                .Where(x => x.UserId == _authContext.UserId)
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

        public async Task<CharacterDto> LearnSkillAsync(Guid id, Guid skillId)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            if (character.CharacterSkills.Count >= 3)
            {
                throw new ServiceException("Character must unlearn a skill before learning another");
            }

            if (character.CharacterSkills.Any(x => x.SkillId == skillId))
            {
                throw new ServiceException("Character has already learned this skill");
            }
            
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == skillId)
                .SingleAsync();

            var characterSkill = new CharacterSkill
            {
                CharacterId = character.Id,
                Character = character,
                SkillId = skill.Id,
                Skill = skill
            };
            _unitOfWork.CharacterSkills.Create(characterSkill);

            await _unitOfWork.CommitAsync();

            return _characterMapper.Map(character);
        }

        public async Task<CharacterDto> UnlearnSkillAsync(Guid id, Guid skillId)
        {
            var character = await _unitOfWork.Characters.Query
                .Where(x => x.UserId == _authContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            if (character.CharacterSkills.Count == 0)
            {
                throw new ServiceException("Character has no skills");
            }

            if (character.CharacterSkills.All(x => x.SkillId != skillId))
            {
                throw new ServiceException("Character has not learned this skill");
            }
            
            var characterSkill = await _unitOfWork.CharacterSkills.Query
                .Where(x => x.CharacterId == id)
                .Where(x => x.SkillId == skillId)
                .SingleAsync();

            _unitOfWork.Characters.Update(character);
            _unitOfWork.CharacterSkills.Delete(characterSkill);

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