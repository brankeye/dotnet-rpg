using System;

namespace dotnet_rpg.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(Guid id) 
            : base($"Record with id \'{id}\' could not be found") 
        {
           
        }
    }
}