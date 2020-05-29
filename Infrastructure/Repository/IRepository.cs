using dotnet_rpg.Infrastructure.Repository.Query;

namespace dotnet_rpg.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IRepositoryQuery<T> Query { get; }

        T Create(T entity);
        
        T Update(T entity);
        
        T Delete(T entity);
    }
}