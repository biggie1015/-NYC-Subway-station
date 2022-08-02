using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SubwayEntrance.Data.EFCore
{
    public abstract class EFCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {

        public readonly TContext context;
        public EFCoreRepository(TContext _context)
        {
            context = _context;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return await context.Set<TEntity>().FindAsync(expression);
        }
    }
}
