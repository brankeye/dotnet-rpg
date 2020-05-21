using System;
using System.Reflection;
using dotnet_rpg.Infrastructure.Enums;

namespace dotnet_rpg.Infrastructure.Exceptions
{
    public class NotFoundException : RepositoryException
    {
        public NotFoundException() 
            : this("Record not found")
        {
            Status = DbStatusCode.NotFound;
        }
        
        public NotFoundException(string message) 
            : base(message)
        {
            Status = DbStatusCode.NotFound;
        }
        
        public NotFoundException(MemberInfo type)
            : this($"{type.Name} not found")
        {
           
        }
        
        public NotFoundException(MemberInfo type, Guid id)
            : this($"{type.Name} with id \'{id}\' not found")
        {
           
        }
        
        public NotFoundException(Guid id)
            : this($"Query for record with id \'{id}\' failed")
        {
           
        }
        
        public NotFoundException(string firstName, Guid firstId, string secondName, Guid secondId) 
            : base($"Record with composite id of {firstName} \'{firstId}\' and {secondName} \'{secondId}\' could not be found") 
        {
           
        }

        public NotFoundException(string paramName, string paramValue)
            : base($"Record with {paramName} \'{paramValue}\' could not be found")
        {

        }
        
        public NotFoundException(string paramName, Guid id)
            : base($"Record with {paramName} \'{id}\' could not be found")
        {

        }
    }
}