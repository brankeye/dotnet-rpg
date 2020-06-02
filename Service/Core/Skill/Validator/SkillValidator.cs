using dotnet_rpg.Service.Core.Skill.Dtos;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Skill.Validator
{
    public class SkillValidator : Skill.Validator.ISkillValidator
    {
        private readonly Validation.IValidator<CreateSkillDto> _createValidator;
        private readonly Validation.IValidator<UpdateSkillDto> _updateValidator;
        
        public SkillValidator()
        {
            _createValidator = new CreateSkillDtoValidator();
            _updateValidator = new UpdateSkillDtoValidator();
        }

        public void ValidateAndThrow(CreateSkillDto entity)
        {
            _createValidator.ValidateAndThrow(entity);
        }

        public void ValidateAndThrow(UpdateSkillDto entity)
        {
            _updateValidator.ValidateAndThrow(entity);
        }
    }
    
    public class CreateSkillDtoValidator : Validation.Validator<CreateSkillDto>
    {
        public CreateSkillDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("A name must be given");
            RuleFor(x => x.Damage)
                .GreaterThan(0)
                .WithMessage("Damage must be greater than 0");
        }
    }
    
    public class UpdateSkillDtoValidator : Validation.Validator<UpdateSkillDto>
    {
        public UpdateSkillDtoValidator()
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