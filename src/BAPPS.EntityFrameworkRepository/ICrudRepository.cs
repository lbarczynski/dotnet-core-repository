using System;
using System.Collections.Generic;
using System.Text;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository
{
    public interface ICrudRepository<TEntity> : ICrudRepository<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }

    public interface ICrudRepository<TEntity, in TID> : IReadOnlyRepository<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
        /// <summary>
        /// Create new entity if not exists or update if entity already exists.
        /// </summary>
        /// <param name="entity">Entity for create or update</param>
        /// <returns>New entity state</returns>
        TEntity CreateOrUpdate(TEntity entity);

        void Delete(TID id);
        /// <summary>
        /// Try to find entity and delete it.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        void Delete(TEntity entity);
        /// <summary>
        /// Save changes in repository
        /// </summary>
        void Save();
    }
}
