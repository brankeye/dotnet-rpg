using System;
using dotnet_rpg.Infrastructure.Enums;

namespace dotnet_rpg.Infrastructure.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message)
            : base(message)
        {
            Status = DbStatusCode.Unknown;
        }
        
        public RepositoryException(string message, Exception exception)
            : base(message, exception)
        {
            Status = DbStatusCode.Unknown;
        }
        
        public DbStatusCode Status { get; set; }
    }
}