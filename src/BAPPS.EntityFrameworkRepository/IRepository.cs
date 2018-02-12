using System;
using System.Collections.Generic;
using System.Text;

namespace BAPPS.EntityFrameworkRepository
{
    public interface IRepository<TEntity, in TID> : IAsyncCrudRepository<TEntity, TID>, ICrudRepository<TEntity, TID>
        where TEntity : class, IEntityIdentifierProvider<TID>
        where TID : struct
    {
    }
}
