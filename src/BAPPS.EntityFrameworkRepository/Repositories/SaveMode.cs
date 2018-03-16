using System;
using System.Collections.Generic;
using System.Text;

namespace BAPPS.EntityFrameworkRepository.Repositories
{
    public enum SaveMode
    {
        /// <summary>
        /// Save changes after call Save method
        /// </summary>
        Explicit,
        /// <summary>
        /// Save changes automatically after all operations
        /// </summary>
        Implicit
    }
}
