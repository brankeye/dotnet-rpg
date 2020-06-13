using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.UnequipSkillCommand
{
    public class UnlearnSkillCommandValidator : Validator<UnlearnSkillCommand>
    {
        public UnlearnSkillCommandValidator()
        {
            RuleFor(x => x.CharacterId)
                .NotEmpty()
                .WithMessage("Character id must be given");
        }
    }
}