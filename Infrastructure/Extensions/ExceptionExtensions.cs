using System;
using dotnet_rpg.Infrastructure.Exceptions;

namespace dotnet_rpg.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static RepositoryException ToRepositoryException(this Exception ex, string fallbackMessage)
        {
            var exception = ex as RepositoryException;
            return exception ?? new RepositoryException(fallbackMessage, ex);
        }
    }
}