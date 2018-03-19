namespace BAPPS.Repository.EntityFramework.Core.Repositories
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
