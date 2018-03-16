using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Entity;
using Microsoft.EntityFrameworkCore;

namespace BAPPS.EntityFrameworkRepository.Context
{
    public class DbContextAdapter : IDbContext
    {
        private readonly DbContext _dbContext;
        public DbContextAdapter(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IDbSet<TEntity, TID> Set<TEntity, TID>() where TEntity : class, IEntity<TID> where TID : struct
        {
            return new DbSetAdapter<TEntity, TID>(_dbContext.Set<TEntity>());
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
