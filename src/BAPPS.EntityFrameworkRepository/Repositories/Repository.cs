using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Entity;
using BAPPS.EntityFrameworkRepository.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BAPPS.EntityFrameworkRepository.Repositories
{
    public class Repository<TEntity> : Repository<TEntity, long>, IRepository<TEntity>
        where TEntity : class, IEntity<long>
    {
        protected Repository(IDbContext dbContext, SaveMode saveMode = SaveMode.Explicit)
            : base(dbContext, saveMode)
        {
        }

        protected Repository(IDbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit)
            : base(dbContext, loggerFactory, saveMode)
        {
        }


        public new static IRepository<TEntity> Create(DbContext dbContext, SaveMode saveMode = SaveMode.Explicit)
        {
            return new Repository<TEntity>(new DbContextAdapter(dbContext), saveMode);
        }

        public new static IRepository<TEntity> Create(DbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit)
        {
            return new Repository<TEntity>(new DbContextAdapter(dbContext), loggerFactory, saveMode);
        }
    }

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

            var shouldUpdate = !Equals(entity.GetID(), default(TID)) && Get(entity.GetID()) != null;
            var shouldCreate = Equals(entity.GetID(), default(TID));
            TEntity toReturn;

            if (shouldUpdate)
            {
                toReturn = _dbSet.Update(entity);
            }
            else if (shouldCreate)
            {
                toReturn = _dbSet.Add(entity);
            }
            else
            {
                throw new EntityFrameworkRepositoryException(
                    "Invalid ID value. Define default value {0} for create or existing database entry ID for update.",
                    default(TID));
            }

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


            var shouldUpdate = !Equals(entity.GetID(), default(TID)) && await GetAsync(entity.GetID()) != null;
            var shouldCreate = Equals(entity.GetID(), default(TID));
            TEntity toReturn;

            if (shouldUpdate)
            {
                toReturn = _dbSet.Update(entity);
            }
            else if (shouldCreate)
            {
                toReturn = await _dbSet.AddAsync(entity);
            }
            else
            {
                throw new EntityFrameworkRepositoryException(
                    "Invalid ID value. Define default value {0} for create or existing database entry ID for update.",
                    default(TID));
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
