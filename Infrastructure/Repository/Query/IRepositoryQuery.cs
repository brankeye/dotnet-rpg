using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace dotnet_rpg.Infrastructure.Repository.Query
{
    public interface IRepositoryQuery<T> where T : class
    {
        IRepositoryQuery<T> Where(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync();
        
        Task<IEnumerable<T>> ToListAsync();
        
        Task<T> SingleAsync();
    }
}