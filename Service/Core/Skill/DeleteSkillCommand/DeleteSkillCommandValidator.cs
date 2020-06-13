using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Skill.DeleteSkillCommand
{
    public class DeleteSkillCommandValidator : Validator<DeleteSkillCommand>
    {
        public DeleteSkillCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("An id must be given");
        }
    }
}