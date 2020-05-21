namespace dotnet_rpg.Service.Validation
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
