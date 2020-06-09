namespace dotnet_rpg.Service.Contracts.Validation
{
    public interface IValidator<in T>
    {
        void ValidateAndThrow(T entity);
    }
}
