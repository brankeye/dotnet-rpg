using System;
using System.Collections.Generic;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Service.Exceptions;

namespace dotnet_rpg.Service.Utility.ExceptionUtility
{
    public static class ExceptionUtility
    {
        private const string DefaultMessage = "Something went wrong";
        
        public static IEnumerable<ServiceError> GetErrors()
        {
            return new List<ServiceError>
            {
                new ServiceError
                {
                    Code = ErrorCode.ServerError,
                    Message = DefaultMessage
                }
            };
        }
        
        public static IEnumerable<ServiceError> GetErrors(string message)
        {
            return new List<ServiceError>
            {
                new ServiceError
                {
                    Code = ErrorCode.ServerError,
                    Message = message,
                }
            };
        }
        
        public static IEnumerable<ServiceError> GetErrors(Exception exception)
        {
            return exception switch
            {
                ValidationException ex => GetErrors(ex),
                {} ex => GetErrors(ex.Message),
                _ => GetErrors(DefaultMessage)
            };
        }
        
        private static IEnumerable<ServiceError> GetErrors(ValidationException exception)
        {
            var errorCode = GetErrorCode(exception);

            var errors = new List<ServiceError>();

            foreach (var validationError in exception.Errors)
            {
                errors.Add(new ServiceError
                {
                    Code = errorCode,
                    Message = validationError.Message
                });
            }

            return errors;
        }

        private static ErrorCode GetErrorCode(Exception ex)
        {
            return ex switch
            {
                ValidationException _ => ErrorCode.ValidationError,
                RepositoryException _ => ErrorCode.DataError,
                _ => ErrorCode.ServerError
            };
        }
    }
}