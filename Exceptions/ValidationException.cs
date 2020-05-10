using System;
using System.Collections.Generic;
using dotnet_rpg.Validators;

namespace dotnet_rpg.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}