using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Skill.CreateSkillCommand
{
    public class CreateSkillCommandValidator : Validator<CreateSkillCommand>
    {
        public CreateSkillCommandValidator()
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