using System;

namespace dotnet_rpg.Service.Exceptions
{
    public class ServiceException : Exception
    {
        protected ServiceException() : base()
        {
            
        }
        
        public ServiceException(string message) : base(message)
        {
            
        }
        
        public ServiceException(string message, Exception ex) : base(message, ex)
        {
            
        }
    }
}