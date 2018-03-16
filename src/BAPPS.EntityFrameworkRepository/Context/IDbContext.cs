using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository.Context
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
