namespace dotnet_rpg.Service.Contracts.Validation
{
    public class NoopValidator<T> : Contracts.Validation.IValidator<T>
    {
        public void ValidateAndThrow(T entity)
        {
            
        }
    }
}