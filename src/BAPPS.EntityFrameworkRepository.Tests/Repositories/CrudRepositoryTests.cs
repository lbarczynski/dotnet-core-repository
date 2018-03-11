using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object);
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
            var id = _testData.Max(q => q.ID) + 100;
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

            // Assert
        }
    }
}
