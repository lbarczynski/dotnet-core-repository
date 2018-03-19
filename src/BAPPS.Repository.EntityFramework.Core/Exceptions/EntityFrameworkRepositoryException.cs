using System;

namespace BAPPS.Repository.EntityFramework.Core.Exceptions
{
    public class EntityFrameworkRepositoryException : ApplicationException
    {
        public EntityFrameworkRepositoryException(string message)
            : base(message)
        {

        }

        public EntityFrameworkRepositoryException(string format, params object[] args)
            : this(string.Format(format, args))
        {

        }
    }
}
