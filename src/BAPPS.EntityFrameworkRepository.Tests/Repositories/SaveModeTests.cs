using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class SaveModeTests : RepositoryTestsBase
    {
        private IRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act
            

            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_CreateNewObject_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_UpdateExistingObject_ShouldCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_CreateOrUpdate_ShouldNotCallSaveMethodForNullEntity()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfIdNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ById_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }


        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldCallSaveMethodAutomaticallyForImplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityNotExists()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForImplicitModeIfEntityIsNull()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Implicit);

            // Act


            // Assert
            Assert.IsTrue(false, "Test not impletemented!");
        }

        [TestMethod]
        public void Repository_Delete_ByEntity_ShouldNotCallSaveMethodAutomaticallyForExplicitMode()
        {
            // Arrange
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object, SaveMode.Explicit);

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
