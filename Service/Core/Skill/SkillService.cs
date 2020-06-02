using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using dotnet_rpg.Infrastructure.UnitOfWork;
using dotnet_rpg.Service.Core.Skill.Dtos;
using dotnet_rpg.Service.Core.Skill.Mapper;
using dotnet_rpg.Service.Core.Skill.Validator;

namespace dotnet_rpg.Service.Core.Skill
{
    public class SkillService : ISkillService
    {
        private readonly IServiceContext _serviceContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISkillValidator _skillValidator;
        private readonly ISkillMapper _skillMapper;

        public SkillService(
            IServiceContext serviceContext, 
            IUnitOfWork unitOfWork,
            ISkillValidator skillValidator) 
        {
            _serviceContext = serviceContext;
            _unitOfWork = unitOfWork;
            _skillValidator = skillValidator;
            _skillMapper = new SkillMapper();
        }

        public async Task<IEnumerable<SkillDto>> GetAllAsync() 
        {
            var weapons = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .ToListAsync();
            return weapons.Select(_skillMapper.Map);
        }

        public async Task<SkillDto> GetByIdAsync(Guid id)
        {
            var weapon = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            return _skillMapper.Map(weapon);
        }

        public async Task<SkillDto> CreateAsync(CreateSkillDto dto) 
        {
            _skillValidator.ValidateAndThrow(dto);
            var newSkill = _skillMapper.Map(dto, _serviceContext.UserId);
            var skill = _unitOfWork.Skills.Create(newSkill);
            await _unitOfWork.CommitAsync();
            return _skillMapper.Map(skill);
        }

        public async Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto dto) 
        {
            _skillValidator.ValidateAndThrow(dto);
            
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();

            UpdateModel(skill, dto);
            _unitOfWork.Skills.Update(skill);
            await _unitOfWork.CommitAsync();
            
            return _skillMapper.Map(skill);
        }

        public async Task DeleteAsync(Guid id) 
        {
            var skill = await _unitOfWork.Skills.Query
                .Where(x => x.UserId == _serviceContext.UserId)
                .Where(x => x.Id == id)
                .SingleAsync();
            _unitOfWork.Skills.Delete(skill);
            await _unitOfWork.CommitAsync();
        }
        
        private static void UpdateModel(Domain.Models.Skill skill, UpdateSkillDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            skill.Name = dto.Name;
            skill.Damage = dto.Damage;
        }
    }
}