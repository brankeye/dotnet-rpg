using dotnet_rpg.Service.Enums;

namespace dotnet_rpg.Service.Contracts.Validation
{
    public class ValidationError
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public ErrorCode ErrorCode => ErrorCode.ValidationError;
    }
}