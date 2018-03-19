using BAPPS.Repository.EntityFramework.Core.Context;
using BAPPS.Repository.EntityFramework.Core.Repositories;
using Microsoft.Extensions.Logging;

namespace BAPPS.Repository.EntityFramework.Core.Tests
{
    public class TestsRepository : Repository<SampleEntity, long>
    {
        public TestsRepository(IDbContext dbContext, SaveMode saveMode = SaveMode.Explicit) : base(dbContext, saveMode)
        {
        }

        public TestsRepository(IDbContext dbContext, ILoggerFactory loggerFactory, SaveMode saveMode = SaveMode.Explicit) : base(dbContext, loggerFactory, saveMode)
        {
        }
    }
}
