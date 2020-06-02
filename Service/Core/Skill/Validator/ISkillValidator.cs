using dotnet_rpg.Service.Core.Skill.Dtos;
using dotnet_rpg.Service.Validation;

namespace dotnet_rpg.Service.Core.Skill.Validator
{
    public interface ISkillValidator
        : IValidator<CreateSkillDto>,
          IValidator<UpdateSkillDto>
    {
        
    }
}
