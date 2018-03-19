using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.Repository.Entity;

namespace BAPPS.Repository.InMemory
{
    public class InMemoryRepository<TEntity> : InMemoryRepository<TEntity, long>, IRepository<TEntity>
        where TEntity : class, IEntity<long>
    {

    }

    public class InMemoryRepository<TEntity, TID> : IRepository<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
        private readonly IDictionary<TID, TEntity> _dataDictionary = new ConcurrentDictionary<TID, TEntity>();

        public Task<IQueryable<TEntity>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(TID id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TID id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get()
        {
            throw new NotImplementedException();
        }

        public TEntity Get(TID id)
        {
            throw new NotImplementedException();
        }

        public TEntity CreateOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TID id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
