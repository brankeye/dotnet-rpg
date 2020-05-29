using dotnet_rpg.Service.Core.Weapon.Dtos;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Weapon.Validator
{
    public class WeaponValidator : IWeaponValidator
    {
        private readonly Validation.IValidator<CreateWeaponDto> _createValidator;
        private readonly Validation.IValidator<UpdateWeaponDto> _updateValidator;
        
        public WeaponValidator()
        {
            _createValidator = new CreateWeaponDtoValidator();
            _updateValidator = new UpdateWeaponDtoValidator();
        }

        public void ValidateAndThrow(CreateWeaponDto entity)
        {
            _createValidator.ValidateAndThrow(entity);
        }

        public void ValidateAndThrow(UpdateWeaponDto entity)
        {
            _updateValidator.ValidateAndThrow(entity);
        }
    }
    
    public class CreateWeaponDtoValidator : Validation.Validator<CreateWeaponDto>
    {
        public CreateWeaponDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("A name must be given");
            RuleFor(x => x.Damage)
                .GreaterThan(0)
                .WithMessage("Damage must be greater than 0");
        }
    }
    
    public class UpdateWeaponDtoValidator : Validation.Validator<UpdateWeaponDto>
    {
        public UpdateWeaponDtoValidator()
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