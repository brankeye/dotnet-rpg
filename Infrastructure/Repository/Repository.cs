using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using dotnet_rpg.Domain.Models;
using dotnet_rpg.Infrastructure.Exceptions;
using dotnet_rpg.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
        
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var exists = await _dbSet.AnyAsync(predicate);
                return exists;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to find entity of type {typeof(T).Name}");
            }
        }
        
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var list = await ModifyQuery(_dbSet).Where(predicate).ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to get entities of type {typeof(T).Name}");
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var characterWeapon = await ModifyQuery(_dbSet)
                    .SingleOrDefaultAsync(predicate);

                if (characterWeapon == null)
                {
                    throw new NotFoundException(typeof(CharacterWeapon));
                }

                return characterWeapon;
            }
            catch (Exception ex)
            {
                throw ex.ToRepositoryException($"Failed to get entity of type {typeof(T).Name}");
            }
        }

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