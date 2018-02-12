﻿using System.Linq;

namespace BAPPS.EntityFrameworkRepository {
    public interface IReadOnlyRepository<out TEntity, in TID>
        where TEntity : class
        where TID : struct
    {
        /// <summary>
        /// Get Queryable for all entities in repository.
        /// </summary>
        /// <returns>Queryable for all entites.</returns>
        IQueryable<TEntity> Get();
        /// <summary>
        /// Find specify entity by ID.
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Found entity, null if not found</returns>
        TEntity Get(TID id);
    }
}
