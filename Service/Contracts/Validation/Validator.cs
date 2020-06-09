using System.Collections.Generic;
using FluentValidation;
using ValidationException = dotnet_rpg.Service.Exceptions.ValidationException;

namespace dotnet_rpg.Service.Contracts.Validation
{
    public abstract class Validator<T> : AbstractValidator<T>, Contracts.Validation.IValidator<T>
    {
        private readonly IList<ValidationError> _errors;

        protected Validator()
        {
            _errors = new List<ValidationError>();
        }

        protected bool IsValid => _errors.Count == 0;

        private IEnumerable<ValidationError> Errors => _errors;

        private void AddError(string message)
        {
            _errors.Add(new ValidationError(message));
        }

        public void ValidateAndThrow(T entity)
        {
            var results = Validate(entity);

            if (results.IsValid)
            {
                return;
            }
            
            foreach (var error in results.Errors)
            {
                AddError(error.ErrorMessage);
            }
            
            throw new ValidationException(Errors);
        }
    }
}
