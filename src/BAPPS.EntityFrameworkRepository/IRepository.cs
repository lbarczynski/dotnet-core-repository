using System;
using System.Collections.Generic;
using System.Text;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository
{
    public interface IRepository<TEntity, in TID> : IAsyncCrudRepository<TEntity, TID>, ICrudRepository<TEntity, TID>
        where TEntity : class, IEntityIdProvider<TID>
        where TID : struct
    {
    }
}
