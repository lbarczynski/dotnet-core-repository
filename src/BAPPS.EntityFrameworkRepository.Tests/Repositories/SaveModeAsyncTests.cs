using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class SaveModeAsyncTests : RepositoryTestsBase
    {
        private IRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
        }


        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_CreateNewObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_CreateNewObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_UpdateExistingObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_CreateOrUpdateAsync_ShouldNotCallSaveMethodForNullEntity()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfIdNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ById_ShouldCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityIsNull()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }


        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_DeleteAsync_ByEntity_ShouldCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_SaveAsync_ShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }


        [TestMethod]
        public async Task Repository_SaveAsync_ShouldThrowExceptionIfRepositoryIsInImplicitMode()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public async Task Repository_SaveAsync_ShouldCallSaveAsyncMethodOnDbContextForValidCall()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }
    }
}
