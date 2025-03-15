using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace AppAwm.DAL
{
    public class RepositoryGeneric<TEntity> : IDisposable, IOperation<TEntity> where TEntity : class
    {
        private bool disposed = false;
        private IDbContextTransaction? transaction = null;
        private DbCon dbContext;
        private DbSet<TEntity> dbSet;

        public RepositoryGeneric(DbCon dataContext, out GenericRepositoryValidation.GenericRepositoryExceptionStatus status)
        {
            status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.Success;
            if (dataContext == null) status = GenericRepositoryValidation.GenericRepositoryExceptionStatus.ArgumentNullException;

            dbContext = dataContext!;
            dbSet = dbContext.Set<TEntity>();

        }

        ~RepositoryGeneric() { Dispose(false); }

        public IDbContextTransaction StartTransaction() => transaction = dbContext.Database.BeginTransaction();

        public void FinishTransactio()
        {
            transaction?.Commit();
            transaction?.Dispose();
        }

        public void RollBackTransaction()
        {
            transaction?.Rollback();
            transaction?.Dispose();
        }

        public int Create(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
                return dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        public int Delete(TEntity Entity)
        {
            try
            {
                dbSet.Remove(Entity);
                return dbContext.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext?.Dispose();
                }
            }

            disposed = true;
        }

        public int Edit(TEntity entity)
        {
            try
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null)
        {
            try
            {
                return dbSet.Where(predicate!).AsNoTracking();
            }
            catch
            {
                throw;
            }
        }

        public TEntity? GetItem(Expression<Func<TEntity, bool>>? predicate = null)
        {
            try
            {
                if (predicate is null)
                    return null;

                return dbSet.AsNoTracking().FirstOrDefault(predicate);
            }
            catch
            {
                throw;
            }
        }

        public int BulkInsert(List<TEntity> entities)
        {
            try
            {
                if (entities.Count == 0) return 0;

                dbSet.AddRange(entities);
                return dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
