using BAPPS.Repository.EntityFramework.Core.DbSet;
using System;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.Repository.Entity;

namespace BAPPS.Repository.EntityFramework.Core.Context
{
    public interface IDbContext : IDisposable
    {
        IDbSet<TEntity, TID> Set<TEntity, TID>()
            where TEntity : class, IEntity<TID>
            where TID : struct;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
