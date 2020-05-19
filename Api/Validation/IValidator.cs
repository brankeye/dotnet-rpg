namespace dotnet_rpg.Api.Validation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
