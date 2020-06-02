using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Service.Core.Skill.Dtos;

namespace dotnet_rpg.Service.Core.Skill
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllAsync();
        
        Task<SkillDto> GetByIdAsync(Guid id);
        
        Task<SkillDto> CreateAsync(CreateSkillDto dto);
        
        Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto dto);
        
        Task DeleteAsync(Guid id);
    }
}