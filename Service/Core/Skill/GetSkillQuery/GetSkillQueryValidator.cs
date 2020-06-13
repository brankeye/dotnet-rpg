using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Skill.GetSkillQuery
{
    public class GetSkillQueryValidator : Validator<GetSkillQuery>
    {
        public GetSkillQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("An id must be given");
        }
    }
}