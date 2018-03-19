using BAPPS.Repository.Entity;
using BAPPS.Repository.EntityFramework.Core.DbSet;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BAPPS.Repository.EntityFramework.Core.Context
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
