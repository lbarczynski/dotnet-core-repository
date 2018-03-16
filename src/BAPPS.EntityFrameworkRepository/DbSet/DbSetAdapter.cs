using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Entity;
using Microsoft.EntityFrameworkCore;

namespace BAPPS.EntityFrameworkRepository.DbSet
{
    public class DbSetAdapter<TEntity, TID> : IDbSet<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
        private readonly DbSet<TEntity> _dbSet;

        public DbSetAdapter(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity Find(TID id)
        {
            return _dbSet.Find(id);
        }

        public Task<TEntity> FindAsync(TID id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.FindAsync(keyValues: new object[] { id }, cancellationToken: cancellationToken);
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity)?.Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entry = await _dbSet.AddAsync(entity, cancellationToken);
            return entry?.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            return _dbSet.Update(entity)?.Entity;
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity)?.Entity;
        }
    }
}
