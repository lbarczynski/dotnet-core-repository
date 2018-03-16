using System;
using System.Collections.Generic;
using System.Text;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.Extensions.Logging;

namespace BAPPS.EntityFrameworkRepository.Tests
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
