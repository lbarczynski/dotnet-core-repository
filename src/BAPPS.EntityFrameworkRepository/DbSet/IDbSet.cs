using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository.DbSet
{
    public interface IDbSet<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
        IQueryable<TEntity> AsQueryable();
        TEntity Find(TID id);
        Task<TEntity> FindAsync(TID id, CancellationToken cancellationToken = default(CancellationToken));
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
    }
}
