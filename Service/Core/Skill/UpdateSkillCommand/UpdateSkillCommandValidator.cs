using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Skill.UpdateSkillCommand
{
    public class UpdateSkillCommandValidator : Validator<UpdateSkillCommand>
    {
        public UpdateSkillCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("A name must be given");
            RuleFor(x => x.Damage)
                .GreaterThan(0)
                .WithMessage("Damage must be greater than 0");
        }
    }
}