using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.DbSet;
using BAPPS.EntityFrameworkRepository.Repositories;
using BAPPS.EntityFrameworkRepository.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class SaveModeAsyncTests : RepositoryTestsBase
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
        public async Task Repository_CreateOrUpdateAsync_CreateNewObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var newEntity = new SampleEntity();

            // Act
            await _repository.CreateOrUpdateAsync(newEntity);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_CreateNewObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var newEntity = new SampleEntity();

            // Act
            await _repository.CreateOrUpdateAsync(newEntity);

            // Assert
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];
            existing.SampleValue = "new value";

            // Act
            await _repository.CreateOrUpdateAsync(existing);

            // Assert            
            _dbSetMock.Verify(q => q.Add(It.IsAny<SampleEntity>()), Times.Never);
            _dbSetMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_UpdateExistingObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];
            existing.SampleValue = "new value";

            // Act
            await _repository.CreateOrUpdateAsync(existing);

            // Assert
            _dbSetMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_ShouldNotCallSaveMethodForNullEntity()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            const SampleEntity newEntity = null;

            // Act
            await _repository.CreateOrUpdateAsync(newEntity);

            // Assert
            _dbSetMock.Verify(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()), Times.Never);
            _dbSetMock.Verify(q => q.Update(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            await _repository.DeleteAsync(existing.ID);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfIdNotExists()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var id = _repository.Get().Max(q => q.ID) + 1;

            // Act
            await _repository.DeleteAsync(id);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            await _repository.DeleteAsync(existing.ID);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            await _repository.DeleteAsync(existing);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityIsNull()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            const SampleEntity entity = null;

            // Act
            await _repository.DeleteAsync(entity);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityNotExists()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var noExistingEntity = new SampleEntity()
            {
                ID = _repository.Get().ToList().Max(q => q.ID) + 1,
                SampleValue = "entity to remove"
            };

            // Act
            await _repository.DeleteAsync(noExistingEntity);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Never);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Explicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);
            var existing = _repository.Get().ToList()[0];

            // Act
            await _repository.DeleteAsync(existing);

            // Assert
            _dbSetMock.Verify(q => q.Remove(It.IsAny<SampleEntity>()), Times.Once);
            _dbContextMock.Verify(q => q.SaveChanges(), Times.Never);
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task Repository_SaveAsync_ShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object);
            using (_repository)
            {
                // just for dispose
            }

            // Act
            await _repository.SaveAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task Repository_SaveAsync_ShouldThrowExceptionIfRepositoryIsInImplicitMode()
        {
            // Arrange
            const SaveMode saveMode = SaveMode.Implicit;
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object, saveMode);

            // Act
            using (_repository)
            {
                await _repository.SaveAsync();
            }
        }

        [TestMethod]
        public async Task Repository_SaveAsync_ShouldCallSaveAsyncMethodOnDbContextForValidCall()
        {
            // Arrange
            _repository = new TestsRepository(_dbContextMock.Object, LoggerFactoryMock.Object);

            // Act
            using (_repository)
            {
                await _repository.SaveAsync();
            }

            // Assert
            _dbContextMock.Verify(q => q.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
