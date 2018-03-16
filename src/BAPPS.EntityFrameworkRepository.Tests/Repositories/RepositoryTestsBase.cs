using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
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
