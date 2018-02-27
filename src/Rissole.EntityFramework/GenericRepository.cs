using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Rissole.EntityFramework
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public virtual DbSet<TEntity> Entities => _dbSet;

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbSet.RemoveRange(_dbSet.Where(predicate));
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = _dbSet.Where(predicate);

            return query;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params string[] includes)
        {
            var query = Get(predicate);

            foreach (var include in includes)
            {
                query = Include(query, include);
            }

            return query;
        }

        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> query, string include)
        {
            return query.Include(include);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }
    }
}
