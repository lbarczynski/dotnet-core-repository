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
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
        private readonly ILogger<Repository<TEntity, TID>> _logger;
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly SaveMode _saveMode;

        public Repository(DbContext dbContext, SaveMode saveMode = SaveMode.Explicit)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
            _saveMode = saveMode;
        }

        public Repository(DbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit) : this(dbContext, saveMode)
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
            _logger?.LogDebug("GetAsync()");
            CheckIfDisposed();
            return Task.Factory.StartNew(() => _dbSet.AsQueryable());
        }

        #endregion

        #region Get by ID

        public TEntity Get(TID id)
        {
            _logger?.LogDebug("Get(id = {id})", id);
            CheckIfDisposed();
            return _dbSet.Find(id);
        }

        public Task<TEntity> GetAsync(TID id)
        {
            _logger?.LogDebug("GetAsync(id = {id})", id);
            CheckIfDisposed();
            return _dbSet.FindAsync(id);
        }

        #endregion

        #region Create or update

        public TEntity CreateOrUpdate(TEntity entity)
        {
            _logger?.LogDebug("CreateOrUpdate(entity = {entity})", entity);
            CheckIfDisposed();
            if (entity == null)
            {
                _logger?.LogWarning("CreateOrUpdate - null value is not expected!");
                return null;
            }

            var exists = Get(entity.GetID()) != null;
            if (!exists) return _dbSet.Add(entity).Entity;
            var updated = _dbSet.Update(entity).Entity;

            if (_saveMode == SaveMode.Implicit)
                Save();

            return updated;
        }

        public async Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            _logger?.LogDebug("CreateOrUpdateAsync(entity = {entity})", entity);
            CheckIfDisposed();
            if (entity == null)
            {
                _logger?.LogWarning("CreateOrUpdateAsync - null value is not expected!");
                return null;
            }

            var exists = (await GetAsync(entity.GetID())) != null;
            if (!exists) return (await _dbSet.AddAsync(entity)).Entity;
            var updated = _dbSet.Update(entity).Entity;

            if (_saveMode == SaveMode.Implicit)
                Save();

            return updated;
        }

        #endregion

        #region Delete

        public void Delete(TID id)
        {
            _logger?.LogDebug("Delete(id = {id})", id);
            CheckIfDisposed();

            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
                _dbSet.Remove(existingEntity);

            if (_saveMode == SaveMode.Implicit)
                Save();
        }

        public async Task DeleteAsync(TID id)
        {
            _logger?.LogDebug("DeleteAsync(id = {id})", id);
            CheckIfDisposed();

            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity != null)
                _dbSet.Remove(existingEntity);

            if (_saveMode == SaveMode.Implicit)
                await SaveAsync();
        }

        public void Delete(TEntity entity)
        {
            _logger?.LogDebug("Delete(entity = {entity})", entity);
            CheckIfDisposed();

            if (entity != null && _dbSet.Find(entity.GetID()) != null)
                _dbSet.Remove(entity);

            if (_saveMode == SaveMode.Implicit)
                Save();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _logger?.LogDebug("DeleteAsync(entity = {entity})", entity);
            CheckIfDisposed();

            if (entity != null && await _dbSet.FindAsync(entity.GetID()) != null)
                _dbSet.Remove(entity);

            if (_saveMode == SaveMode.Implicit)
                await SaveAsync();
        }

        #endregion

        #region Save

        public void Save()
        {
            CheckIfDisposed();
            _logger?.LogDebug("Save()");
            var count = _dbContext.SaveChanges();
            _logger?.LogDebug("Save() => {count} changes saved", count);
        }

        public async Task SaveAsync()
        {
            CheckIfDisposed();
            _logger?.LogDebug("SaveAsync()");
            var count = await _dbContext.SaveChangesAsync();
            _logger?.LogDebug("SaveAsync() => {count} changes saved", count);
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
