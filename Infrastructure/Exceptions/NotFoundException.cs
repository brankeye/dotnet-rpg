using System;

namespace dotnet_rpg.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() 
            : base("Record not found") 
        {
           
        }
        
        public NotFoundException(Guid id) 
            : base($"Record with id \'{id}\' could not be found") 
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