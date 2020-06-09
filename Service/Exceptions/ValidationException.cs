using System.Collections.Generic;
using dotnet_rpg.Service.Contracts.Validation;
using dotnet_rpg.Service.Enums;

namespace dotnet_rpg.Service.Exceptions
{
    public class ValidationException : ServiceException
    {
        public ValidationException(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
