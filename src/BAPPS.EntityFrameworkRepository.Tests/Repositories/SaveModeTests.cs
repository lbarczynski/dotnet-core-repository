using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class SaveModeTests
    {
        private IRepository<SampleEntity, long> _repository;
        private Mock<ILoggerFactory> _loggerFactoryMock = new Mock<ILoggerFactory>();
        private Mock<ILogger> _loggerMock = new Mock<ILogger>();
        private Mock<DbContext> _dbContextMock = new Mock<DbContext>();
        private Mock<DbSet<SampleEntity>> _dbSetMock = new Mock<DbSet<SampleEntity>>();
        private IList<SampleEntity> _testTempEntities = new List<SampleEntity>();

        [TestInitialize]
        public void SetUp()
        {
            _dbContextMock.Setup(q => q.Add(It.IsAny<SampleEntity>()))
                .Callback<SampleEntity>(entity => _testTempEntities.Add(entity))
                .Returns(Mock.Of<EntityEntry<SampleEntity>>());

            _dbContextMock.Setup(q => q.Set<SampleEntity>()).Returns(_dbSetMock.Object);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, saveMode);
            var newEntity = new SampleEntity();

            // Act
            _repository.CreateOrUpdate(newEntity);

            // Assert
            _dbContextMock.Verify(q => q.SaveChanges());
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_ShouldNotCallSaveMethodForNullEntity()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfIdNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }


        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityIsNull()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_dbContextMock.Object, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Save_ShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }


        [TestMethod]
        public void Repository_Save_ShouldThrowExceptionIfRepositoryIsInImplicitMode()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Save_ShouldCallSaveMethodOnDbContextForValidCall()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }
    }
}
