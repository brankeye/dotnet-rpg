using dotnet_rpg.Service.Enums;

namespace dotnet_rpg.Service.Utility.ExceptionUtility
{
    public class ServiceError
    { 
        public ErrorCode Code { get; set; }
        
        public string Message { get; set; }
    }
}