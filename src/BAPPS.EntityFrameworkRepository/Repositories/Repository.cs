using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BAPPS.EntityFrameworkRepository.Repositories
{
    public class Repository<TEntity, TID> : IRepository<TEntity, TID>, IDisposable
        where TEntity : class, IEntityIdentifierProvider<TID>
        where TID : struct
    {

        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        #region Get all

        public IQueryable<TEntity> Get()
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAsync()
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        #region Get by ID

        public TEntity Get(TID id)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(TID id)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        #region Create or update

        public TEntity CreateOrUpdate(TEntity entity)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        public Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        #region Delete

        public void Delete(TEntity entity)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        #region Save

        public void Save()
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            CheckIfDisposed();
            throw new NotImplementedException();
        }

        #endregion

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(_disposed) return;
            if (disposing)
            {
                _dbContext?.Dispose();
                _disposed = true;
            }
        }

        private void CheckIfDisposed()
        {
            if(_disposed)
                throw new ObjectDisposedException(GetType().FullName, "Repository disposed");
        }
    }
}
