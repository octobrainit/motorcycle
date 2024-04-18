using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Motorcycle.App.Api.Context.Base
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : Database
    {
        protected readonly TContext Context;
        protected BaseRepository(TContext context)
        {
            Context = context;
        }

        public async virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, bool notracking = false) => 
            notracking ? 
                await Context.Set<TEntity>().Where(predicate).ToListAsync() :
                await Context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();

        public async virtual Task Add(TEntity entity) => await Context.Set<TEntity>().AddAsync(entity);

        public async virtual Task Update(TEntity entity) => Context.Entry(entity).State = EntityState.Modified;

        public async virtual Task Delete(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public async virtual Task SaveChanges() => await Context.SaveChangesAsync();
    }
}
