using System;
using System.Linq;
using dotnet_rpg.Infrastructure.Extensions;
using dotnet_rpg.Infrastructure.Repository.Query;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        
        protected Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        protected virtual IQueryable<T> ModifyQuery(IQueryable<T> queryable)
        {
            return queryable;
        }
        
        public IRepositoryQuery<T> Query => new RepositoryQuery<T>(ModifyQuery(_dbSet));

        public void Create(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to create entity of type {typeof(T).Name}");
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
            
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to update entity of type {typeof(T).Name}");
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to delete entity of type {typeof(T).Name}");
            }
        }
    }
}