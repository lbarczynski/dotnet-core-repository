using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Repositories;
using BAPPS.EntityFrameworkRepository.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class SaveModeTests : RepositoryTestsBase
    {
        private IRepository<SampleEntity, long> _repository;
        private readonly Mock<IDbContext> _dbContextMock = new Mock<IDbContext>();
        private Mock<IDbSet<SampleEntity, long>> _dbSetMock;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _dbSetMock = MockHelpers.GetDbSetMock(TestData);
            _dbContextMock.Setup(q => q.Set<SampleEntity, long>()).Returns(_dbSetMock.Object);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var newEntity = new SampleEntity();

            // Act
            _repository.CreateOrUpdate(newEntity);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Once);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var newEntity = new SampleEntity();

            // Act
            _repository.CreateOrUpdate(newEntity);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Once);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];
            existing.SampleValue = "new value";

            // Act
            _repository.CreateOrUpdate(existing);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];
            existing.SampleValue = "new value";

            // Act
            _repository.CreateOrUpdate(existing);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_ShouldNotCallSaveMethodForNullEntity()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            const SampleEntity newEntity = null;

            // Act
            _repository.CreateOrUpdate(newEntity);


            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            _repository.Delete(existing.ID);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfIdNotExists()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var id = _repository.Get().Max(q => q.ID) + 1;

            // Act
            _repository.Delete(id);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            _repository.Delete(existing.ID);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }


        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            _repository.Delete(existing);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityNotExists()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var notExistingEntity = new SampleEntity();

            // Act
            _repository.Delete(notExistingEntity);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityIsNull()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            const SampleEntity entity = null;

            // Act
            _repository.Delete(entity);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            _repository.Delete(existing);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void Repository_Save_ShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object);
            using (_repository)
            {
                // just for dispose
            }

            // Act
            _repository.Save();
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Repository_Save_ShouldThrowExceptionIfRepositoryIsInImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);

            // Act
            using (_repository)
            {
                _repository.Save();
            }
        }

        [TestMethod]
        public void Repository_Save_ShouldCallSaveMethodOnDbContextForValidCall()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            _repository.Delete(existing);
            _repository.Save();

            // Assert
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Once);
        }
    }
}
