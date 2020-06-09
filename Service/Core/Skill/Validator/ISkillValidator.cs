using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Core.Skill.Dtos;

namespace dotnet_rpg.Service.Core.Skill.Validator
{
    public interface ISkillValidator
    {
        void ValidateAndThrow(CreateSkillDto entity);
        
        void ValidateAndThrow(UpdateSkillDto entity);
    }
}
