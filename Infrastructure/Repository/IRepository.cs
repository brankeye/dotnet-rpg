using dotnet_rpg.Infrastructure.Repository.Query;

namespace dotnet_rpg.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IRepositoryQuery<T> Query { get; }

        void Create(T entity);
        
        void Update(T entity);
        
        void Delete(T entity);
    }
}