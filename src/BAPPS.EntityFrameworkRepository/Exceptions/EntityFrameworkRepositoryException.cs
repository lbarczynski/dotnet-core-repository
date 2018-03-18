using System;
using System.Collections.Generic;
using System.Text;

namespace BAPPS.EntityFrameworkRepository.Exceptions
{
    public class EntityFrameworkRepositoryException : ApplicationException
    {
        public EntityFrameworkRepositoryException(string message)
            : base(message)
        {

        }

        public EntityFrameworkRepositoryException(string format, params object[] args)
            : this(String.Format(format, args))
        {

        }
    }
}
