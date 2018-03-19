using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.Repository.Entity;

namespace BAPPS.Repository.EntityFramework.Core.DbSet
{
    public interface IDbSet<TEntity, in TID>
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
