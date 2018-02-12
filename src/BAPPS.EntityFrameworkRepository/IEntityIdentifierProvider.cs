using System;
using System.Collections.Generic;
using System.Text;

namespace BAPPS.EntityFrameworkRepository
{
    public interface IEntityIdentifierProvider<TID>
        where TID : struct
    {
        /// <summary>
        /// Provide entity ID
        /// </summary>
        /// <returns></returns>
        TID? GetID();
    }
}
