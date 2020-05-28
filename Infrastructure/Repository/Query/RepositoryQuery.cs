using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository.Query
{
    public class RepositoryQuery<T> : IRepositoryQuery<T>
        where T : class
    {
        private readonly IQueryable<T> _queryable;
        private readonly IList<Expression<Func<T, bool>>> _wherePredicates;

        public RepositoryQuery(IQueryable<T> queryable)
        {
            _queryable = queryable;
            _wherePredicates = new List<Expression<Func<T, bool>>>();
        }

        public IRepositoryQuery<T> Where(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            _wherePredicates.Add(predicate);
            
            return this;
        }

        public async Task<bool> ExistsAsync()
        {
            
            try
            {
                var exists = await Build().AnyAsync();
                return exists;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to find entity of type {typeof(T).Name}");
            }
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            try
            {
                var entities = await Build().ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to get entities of type {typeof(T).Name}");
            }
        }

        public async Task<T> SingleAsync()
        {
            try
            {
                var entity = await Build().SingleOrDefaultAsync();

                if (entity == null)
                {
                    throw new NotFoundException(typeof(T));
                }
                
                return entity;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to get entity of type {typeof(T).Name}");
            }
        }

        private IQueryable<T> Build()
        {
            var queryable = _queryable;
            
            foreach (var predicate in _wherePredicates)
            {
                queryable = queryable.Where(predicate);
            }

            return queryable;
        }
    }
}