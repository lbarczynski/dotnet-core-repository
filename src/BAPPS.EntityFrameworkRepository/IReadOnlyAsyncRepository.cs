using System;
using System.Linq;
using System.Threading.Tasks;

namespace BAPPS.EntityFrameworkRepository
{
    public interface IReadOnlyAsyncRepository<TEntity> : IReadOnlyAsyncRepository<TEntity, long>
        where TEntity : class
    {

    }

    public interface IReadOnlyAsyncRepository<TEntity, in TID> : IDisposable 
        where TEntity : class
        where TID : struct
    {
        /// <summary>
        /// Get Queryable for all entities in repository.
        /// </summary>
        /// <returns>Queryable for all entites.</returns>
        Task<IQueryable<TEntity>> GetAsync();
        /// <summary>
        /// Find specify entity by ID.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Found entity, null if not found</returns>
        Task<TEntity> GetAsync(TID id);
    }
}
