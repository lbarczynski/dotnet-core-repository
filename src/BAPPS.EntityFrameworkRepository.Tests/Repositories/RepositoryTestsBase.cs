using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    public class RepositoryTestsBase
    {
        protected readonly TestDatabaseContext _databaseContext = TestDatabaseContext.Create().WithData();
        protected IList<SampleEntity> _testData;
        protected Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>();
        protected Mock<ILogger> _loggerMock = new Mock<ILogger>();

        public virtual void SetUp()
        {
            _testData = _databaseContext.TestEntities.ToList();
            _loggerFactoryMock.Setup(q => q.CreateLogger(It.IsAny<String>())).Returns(_loggerMock.Object);
        }

    }
}
