using dotnet_rpg.Service.Exceptions;

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