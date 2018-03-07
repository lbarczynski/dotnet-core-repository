using System;
using System.Collections.Generic;
using System.Text;

namespace BAPPS.EntityFrameworkRepository.Entity
{
    public interface IEntityIdProvider<TID>
        where TID : struct
    {
        /// <summary>
        /// Get Entity ID value
        /// </summary>
        /// <returns>Entity ID</returns>
        TID? GetID();
    }
}
