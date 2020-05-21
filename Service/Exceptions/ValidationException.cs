using System;
using System.Collections.Generic;
using dotnet_rpg.Service.Validation;

namespace dotnet_rpg.Service.Exceptions
{
    public class ValidationException : ServiceException
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
}
