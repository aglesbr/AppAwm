using System.Linq.Expressions;

namespace AppAwm.DAL
{
    public interface IOperation<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity>? GetAll(Expression<Func<TEntity, bool>>? predicate = null);
        TEntity GetItem(Expression<Func<TEntity, bool>>? predicate = null);

        int Create(TEntity entity);
        int Edit(TEntity entity);
        int Delete(TEntity Entity);
        int BulkInsert(List<TEntity> entities);
    }
}
