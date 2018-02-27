using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Rissole.EntityFramework
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params string[] includes);

        DbSet<TEntity> Entities { get; }

        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}
