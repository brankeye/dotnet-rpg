using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_rpg.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<IList<T>> GetAllAsync(Guid userId);

        Task<T> GetByIdAsync(Guid userId, Guid id);
        
        Task<T> CreateAsync(Guid userId, T entity);

        Task<T> UpdateAsync(Guid userId, Guid id, T entity);

        Task<T> DeleteAsync(Guid userId, Guid id);
    }
}