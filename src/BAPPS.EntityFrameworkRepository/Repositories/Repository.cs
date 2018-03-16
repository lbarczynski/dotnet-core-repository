using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.DbSet;
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
        private readonly IDbContext _dbContext;
        private readonly IDbSet<TEntity, TID> _dbSet;
        private readonly SaveMode _saveMode;

        public static IRepository<TEntity, TID> Create(DbContext dbContext, SaveMode saveMode = SaveMode.Explicit)
        {
            return new Repository<TEntity, TID>(new DbContextAdapter(dbContext), saveMode);
        }

        public static IRepository<TEntity, TID> Create(DbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit)
        {
            return new Repository<TEntity, TID>(new DbContextAdapter(dbContext), loggerFactory, saveMode);
        }

        protected Repository(IDbContext dbContext, SaveMode saveMode = SaveMode.Explicit)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity, TID>();
            _saveMode = saveMode;
        }

        protected Repository(IDbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit) : this(dbContext, saveMode)
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
            var toReturn = !exists ? _dbSet.Add(entity) : _dbSet.Update(entity);

            if (_saveMode == SaveMode.Implicit)
                Save(internalCall: true);

            return toReturn;
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
            TEntity toReturn;
            if (!exists)
            {
                var entry = await _dbSet.AddAsync(entity);
                toReturn = entry;
            }
            else
            {
                var entry = _dbSet.Update(entity);
                toReturn = entry;
            }

            if (toReturn != null && _saveMode == SaveMode.Implicit)
            {
                await SaveAsync(internalCall: true);
            }

            return toReturn;
        }

        #endregion

        #region Delete

        public void Delete(TID id)
        {
            _logger?.LogDebug("Delete(id = {id})", id);
            CheckIfDisposed();

            var existingEntity = _dbSet.Find(id);
            if (existingEntity == null) return;

            _dbSet.Remove(existingEntity);
            if (_saveMode == SaveMode.Implicit)
                Save(internalCall: true);
        }

        public async Task DeleteAsync(TID id)
        {
            _logger?.LogDebug("DeleteAsync(id = {id})", id);
            CheckIfDisposed();

            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null) return;

            _dbSet.Remove(existingEntity);
            if (_saveMode == SaveMode.Implicit)
                await SaveAsync(internalCall: true);
        }

        public void Delete(TEntity entity)
        {
            _logger?.LogDebug("Delete(entity = {entity})", entity);
            CheckIfDisposed();

            if (entity == null || _dbSet.Find(entity.GetID()) == null) return;

            _dbSet.Remove(entity);
            if (_saveMode == SaveMode.Implicit)
                Save(internalCall: true);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _logger?.LogDebug("DeleteAsync(entity = {entity})", entity);
            CheckIfDisposed();

            if (entity == null || await _dbSet.FindAsync(entity.GetID()) == null) return;

            _dbSet.Remove(entity);
            if (_saveMode == SaveMode.Implicit)
                await SaveAsync(internalCall: true);
        }

        #endregion

        #region Save

        public void Save()
        {
            Save(false);
        }

        public Task SaveAsync()
        {
            return SaveAsync(false);
        }

        private void Save(bool internalCall)
        {
            CheckIfDisposed();
            if (!internalCall && _saveMode == SaveMode.Implicit)
                throw new InvalidOperationException("Unable to save repository in implicit mode");

            _logger?.LogDebug("Save()");
            _dbContext.SaveChanges();
        }

        private Task SaveAsync(bool internalCall)
        {
            CheckIfDisposed();
            if (!internalCall && _saveMode == SaveMode.Implicit)
                throw new InvalidOperationException("Unable to save repository in implicit mode");

            _logger?.LogDebug("SaveAsync()");
            return _dbContext.SaveChangesAsync();
        }

        #endregion

        private bool _disposed = false;

        public void Dispose()
        {
            if (_disposed) return;
            _dbContext?.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void CheckIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().FullName, "Repository disposed");
        }
    }
}
