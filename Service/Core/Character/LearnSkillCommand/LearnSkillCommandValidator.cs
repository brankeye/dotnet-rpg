using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.LearnSkillCommand
{
    public class LearnSkillCommandValidator : Validator<LearnSkillCommand>
    {
        public LearnSkillCommandValidator()
        {
            RuleFor(x => x.CharacterId)
                .NotEmpty()
                .WithMessage("Character id must be given");
            RuleFor(x => x.SkillId)
                .NotEmpty()
                .WithMessage("Skill id must be given");
        }
    }
}