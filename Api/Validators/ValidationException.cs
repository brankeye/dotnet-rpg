using System;
using System.Collections.Generic;

namespace dotnet_rpg.Api.Validators
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        {
            Errors = new List<ValidationError>
            {
                new ValidationError(message)
            };
        }

        public ValidationException(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; }
    }

    public class ValidationError
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
