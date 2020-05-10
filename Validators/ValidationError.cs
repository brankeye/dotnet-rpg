namespace dotnet_rpg.Validators
{
    public class ValidationError
    {
        public ValidationError(string message) {
            Message = message;
        }

        public string Message { get; set; }
    }
}