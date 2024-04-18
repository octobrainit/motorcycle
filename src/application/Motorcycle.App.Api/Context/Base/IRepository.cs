using System.Linq.Expressions;

namespace Motorcycle.App.Api.Context.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, bool notracking = false);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task SaveChanges();
    }
}
