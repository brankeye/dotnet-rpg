using System.Collections.Generic;

namespace dotnet_rpg.Api.Validation
{
    public abstract class Validator
    {
        private readonly IList<ValidationError> _errors;

        protected Validator()
        {
            _errors = new List<ValidationError>();
        }

        protected bool IsValid => _errors.Count == 0;

        protected IList<ValidationError> Errors => _errors;

        protected void AddError(string message)
        {
            _errors.Add(new ValidationError(message));
        }
    }
}
