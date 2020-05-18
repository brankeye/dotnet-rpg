using System;

namespace dotnet_rpg.Api.Validators
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
