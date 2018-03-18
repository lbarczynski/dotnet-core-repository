using System;
using System.Collections.Generic;
using System.Text;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository
{
    public interface IRepository<TEntity> : IRepository<TEntity, long>
        where TEntity : class, IEntity<long>
    {

    }

    public interface IRepository<TEntity, in TID> : ICrudAsyncRepository<TEntity, TID>, ICrudRepository<TEntity, TID>
        where TEntity : class, IEntity<TID>
        where TID : struct
    {
    }
}
