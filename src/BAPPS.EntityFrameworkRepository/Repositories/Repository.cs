using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BAPPS.EntityFrameworkRepository.Repositories
{
    public class Repository<TEntity, TID> : IRepository<TEntity, TID>
        where TEntity : class, IEntityIdProvider<TID>
        where TID : struct
    {
        private readonly ILogger<Repository<TEntity, TID>> _logger;
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public Repository(DbContext dbContext, ILoggerFactory loggerFactory) : this(dbContext)
        {
            _logger = loggerFactory.CreateLogger<Repository<TEntity, TID>>();
        }

        #region Get all

        public IQueryable<TEntity> Get()
        {
            _logger?.LogDebug("Get()");
            CheckIfDisposed();
            return _dbSet.AsQueryable();
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
            _logger?.LogDebug("Get(id = {0})", id);
            CheckIfDisposed();
            return _dbSet.Find(id);
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
            if (_disposed) return;
            if (disposing)
            {
                _dbContext?.Dispose();
                _disposed = true;
            }
        }

        private void CheckIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName, "Repository disposed");
        }
    }
}
