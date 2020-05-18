using System;
using System.Collections.Generic;
using dotnet_rpg.Infrastructure.Exceptions;

namespace dotnet_rpg.Api.Validators
{
    public abstract class Validator
    {
        private readonly IList<ValidationError> _errors;

        public Validator()
        {
            _errors = new List<ValidationError>();
        }

        public bool IsValid => _errors.Count == 0;

        public IList<ValidationError> Errors => _errors;

        public void AddError(string message)
        {
            _errors.Add(new ValidationError(message));
        }
    }
}
