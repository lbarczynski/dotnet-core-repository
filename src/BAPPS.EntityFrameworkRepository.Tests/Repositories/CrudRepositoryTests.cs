using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAPPS.EntityFrameworkRepository.Context;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class CrudRepositoryTests : RepositoryTestsBase
    {
        private ICrudRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new TestsRepository(new DbContextAdapter(TestDatabaseContext), LoggerFactoryMock.Object);
        }

        [TestMethod]
        public void ReadOnlyRepository_CreateOrUpdate_ShouldUpdateObjectIfFoundObjectWithTheSameId()
        {
            using (_repository)
            {
                // Arrange
                var existingObject = _repository.Get().ToList()[0];
                var expectedValue = "new value";
                existingObject.SampleValue = expectedValue;

                // Act
                SampleEntity updatedObject = _repository.CreateOrUpdate(existingObject);

                // Assert
                Assert.IsNotNull(updatedObject);
                Assert.IsTrue(updatedObject.ID > 0);
                Assert.AreEqual(existingObject.ID, updatedObject.ID);
                Assert.AreEqual(existingObject, updatedObject);
            }
        }

        [TestMethod]
        public void ReadOnlyRepository_CreateOrUpdate_ShouldCreateObjectIfIdIsDefault()
        {
            // Arrange
            var newEntity = new SampleEntity()
            {
                ID = default(long), // just for clarity
                SampleValue = "new value"
            };

            // Act
            SampleEntity createdEntity;
            using (_repository)
            {
                createdEntity = _repository.CreateOrUpdate(newEntity);
            }

            // Assert
            Assert.IsNotNull(createdEntity);
            Assert.IsTrue(createdEntity.ID > 0);
            Assert.AreEqual(newEntity.SampleValue, createdEntity.SampleValue);
        }

        [TestMethod]
        public void ReadOnlyRepository_CreateOrUpdate_ShouldCreateObjectWithSpecifiedIdIfNotExistsYet()
        {
            // Arrange
            var id = TestData.Max(q => q.ID) + 100;
            var newEntity = new SampleEntity()
            {
                ID = id,
                SampleValue = "new value"
            };

            // Act
            SampleEntity createdEntity;
            using (_repository)
            {
                createdEntity = _repository.CreateOrUpdate(newEntity);
            }

            // Assert
            Assert.IsNotNull(createdEntity);
            Assert.AreEqual(expected: newEntity, actual: createdEntity);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadOnlyRepository_CreateOrUpdate_ShouldThrowExcetionIfRepositoryIsDisposed()
        {
            // Arrange
            using (_repository)
            {
                // just for dispose
            }

            // Act
            _repository.Get();
        }

        [TestMethod]
        public void ReadOnlyRepository_Delete_ByIdShouldDeleteObjectIfIdExists()
        {
            // Arrange
            var id = _repository.Get().ToList()[0].ID;

            // Act
            _repository.Delete(id);
            _repository.Save();
            var actualValue = _repository.Get(id);

            // Assert
            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public void ReadOnlyRepository_Delete_ByEntityShouldDeleteObjectIfExists()
        {
            // Arrange
            var existingEntity = _repository.Get().ToList()[0];
            var id = existingEntity.ID;

            // Act
            _repository.Delete(existingEntity);
            _repository.Save();
            var actualValue = _repository.Get(id);

            // Assert
            Assert.IsNull(actualValue);
        }


        [TestMethod]
        public void ReadOnlyRepository_Delete_ByIdShouldDoNothingIfIdNotExists()
        {
            // Arrange
            var notValidId = _repository.Get().Max(q => q.ID) + 1;
            var expectedItemsCount = _repository.Get().Count();

            // Act
            _repository.Delete(notValidId);
            _repository.Save();
            var actualItemsCount = _repository.Get().Count();

            // Assert
            Assert.AreEqual(expectedItemsCount, actualItemsCount);
        }

        [TestMethod]
        public void ReadOnlyRepository_Delete_ByEntityShouldDoNothingIfEntityNotExists()
        {
            // Arrange
            var notExistingEntity = new SampleEntity() { SampleValue = "not existing entity" };
            var expectedItemsCount = _repository.Get().Count();

            // Act
            _repository.Delete(notExistingEntity);
            _repository.Save();
            var actualItemsCount = _repository.Get().Count();

            // Assert
            Assert.AreEqual(expectedItemsCount, actualItemsCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadOnlyRepository_Delete_ByIdShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            long id;
            using (_repository)
            {
                id = _repository.Get().ToList()[0].ID;
            }

            // Act
            _repository.Delete(id);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadOnlyRepository_Delete_ByEntityShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            SampleEntity sampleEntity;
            using (_repository)
            {
                sampleEntity = _repository.Get().ToList()[0];
            }

            // Act
            _repository.Delete(sampleEntity);
        }

        [TestMethod]
        public void SecondaryCallForDisposeMethodShouldDoNothing()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();

            // Act
            using (_repository = Repository<SampleEntity, long>.Create(dbContextMock.Object))
            {
                // just for dispose method call
            }
            _repository.Dispose();

            // Assert
            dbContextMock.Verify(q => q.Dispose(), Times.Once);
        }
    }
}
