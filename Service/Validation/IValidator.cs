namespace dotnet_rpg.Service.Validation
{
    public interface IValidator<in T>
    {
        void ValidateAndThrow(T entity);
    }
}
