using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]

    public class CreateMethodsTests : RepositoryTestsBase
    {
        [TestMethod]
        public void CreateMethodShouldReturnNewInstanceEveryTime()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var saveMode = SaveMode.Implicit;

            // Act
            var firstInstance = Repository<SampleEntity, long>.Create(dbContextMock.Object, saveMode);
            var secondInstance = Repository<SampleEntity, long>.Create(dbContextMock.Object, saveMode);

            // Assert
            Assert.AreNotSame(firstInstance, secondInstance);
        }

        [TestMethod]
        public void CreateMethodWithoutIdTypeShouldReturnLongType()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var saveMode = SaveMode.Implicit;

            // Act
            var firstInstance = Repository<SampleEntity>.Create(dbContextMock.Object, saveMode);

            // Assert
            Assert.IsTrue(firstInstance is Repository<SampleEntity, long>);
        }

        [TestMethod]
        public void CreateWithLoggerFactoryMethodWithoutIdTypeShouldReturnLongType()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var saveMode = SaveMode.Implicit;

            // Act
            var firstInstance = Repository<SampleEntity>.Create(dbContextMock.Object, LoggerFactoryMock.Object, saveMode);

            // Assert
            Assert.IsTrue(firstInstance is Repository<SampleEntity, long>);
        }

        [TestMethod]
        public void CreateWithLoggerFactoryMethodShouldReturnNewInstanceEveryTime()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var saveMode = SaveMode.Implicit;

            // Act
            var firstInstance = Repository<SampleEntity, long>.Create(dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var secondInstance = Repository<SampleEntity, long>.Create(dbContextMock.Object, LoggerFactoryMock.Object, saveMode);

            // Assert
            Assert.AreNotSame(firstInstance, secondInstance);
        }

        [TestMethod]
        public void CreateMethodShouldNotCallAnyActionsOnDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var saveMode = SaveMode.Implicit;

            // Act
            var repository = Repository<SampleEntity, long>.Create(dbContextMock.Object, LoggerFactoryMock.Object, saveMode);

            // Assert
            dbContextMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            dbContextMock.Verify(q => q.AddRange(It.IsAny<SampleEntity>()), Times.Never);

            dbContextMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            dbContextMock.Verify(q => q.UpdateRange(It.IsAny<SampleEntity>()), Times.Never);

            dbContextMock.Verify(q => q.Attach(It.IsAny<SampleEntity>()), Times.Never);
            dbContextMock.Verify(q => q.AttachRange(It.IsAny<SampleEntity>()), Times.Never);

            dbContextMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            dbContextMock.Verify(q => q.RemoveRange(It.IsAny<SampleEntity>()), Times.Never);

            dbContextMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            dbContextMock.Verify(q => q.AddRangeAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Never);

            dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
