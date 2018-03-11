using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository
{
    public interface ICrudAsyncRepository<TEntity, in TID> : IReadOnlyAsyncRepository<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct 
    {
        /// <summary>
        /// Create new entity if not exists or update if entity already exists.
        /// </summary>
        /// <param name="entity">Entity for create or update</param>
        /// <returns>New entity state</returns>
        Task<TEntity> CreateOrUpdateAsync(TEntity entity);
        Task DeleteAsync(TID id);
        /// <summary>
        /// Try to find entity and delete it.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Save changes in repository
        /// </summary>
        Task SaveAsync();
    }
}
