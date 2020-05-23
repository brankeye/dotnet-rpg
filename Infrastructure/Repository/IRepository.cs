using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnet_rpg.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        T Create(T entity);
        
        T Update(T entity);
        
        T Delete(T entity);
    }
}