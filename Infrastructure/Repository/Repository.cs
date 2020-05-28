using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
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

        public T Create(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                var character = _dbSet.Add(entity);

                return character.Entity;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to create entity of type {typeof(T).Name}");
            }
        }

        public T Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
            
                var result = _dbSet.Update(entity);

                return result.Entity;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to update entity of type {typeof(T).Name}");
            }
        }

        public T Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                
                _dbSet.Remove(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to delete entity of type {typeof(T).Name}");
            }
        }
    }
}