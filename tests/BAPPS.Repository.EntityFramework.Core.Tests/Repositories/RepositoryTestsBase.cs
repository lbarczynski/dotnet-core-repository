using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;

namespace BAPPS.Repository.EntityFramework.Core.Tests.Repositories
{
    public class RepositoryTestsBase
    {
        protected readonly TestDatabaseContext TestDatabaseContext = TestDatabaseContext.Create().WithData();
        protected IList<SampleEntity> TestData;
        protected Mock<ILoggerFactory> LoggerFactoryMock = new Mock<ILoggerFactory>();
        protected Mock<ILogger> LoggerMock = new Mock<ILogger>();

        public virtual void SetUp()
        {
            TestData = GetTestData(TestDatabaseContext.TestEntities);
            LoggerFactoryMock.Setup(q => q.CreateLogger(It.IsAny<String>())).Returns(LoggerMock.Object);
        }

        private IList<SampleEntity> GetTestData(DbSet<SampleEntity> dbSet)
        {
            var newList = new List<SampleEntity>();
            foreach (var sampleEntity in dbSet)
            {
                newList.Add(sampleEntity.Clone());
            }

            return newList;
        }
    }
}
