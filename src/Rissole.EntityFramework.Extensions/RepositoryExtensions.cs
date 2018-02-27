using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rissole.EntityFramework.Extensions
{
    public static class RepositoryExtensions
    {
        public static Lazy<IGenericRepository<TEntity>> GetLazyRepository<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            return new Lazy<IGenericRepository<TEntity>>(() => new GenericRepository<TEntity>(dbSet));
        }
    }
}
