using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class CrudAsyncRepositoryTests : RepositoryTestsBase
    {
        private IAsyncCrudRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object);
        }

        [TestMethod]
        public async Task ReadOnlyRepository_CreateOrUpdateAsync_ShouldUpdateObjectIfFoundObjectWithTheSameId()
        {
            using (_repository)
            {
                // Arrange
                var existingObject = await _repository.GetAsync(1);
                var expectedValue = "new value";
                existingObject.SampleValue = expectedValue;

                // Act
                SampleEntity updatedObject = await _repository.CreateOrUpdateAsync(existingObject);

                // Assert
                Assert.IsNotNull(updatedObject);
                Assert.IsTrue(updatedObject.ID > 0);
                Assert.AreEqual(existingObject.ID, updatedObject.ID);
                Assert.AreEqual(existingObject, updatedObject);
            }
        }

        [TestMethod]
        public async Task ReadOnlyRepository_CreateOrUpdateAsync_ShouldCreateObjectIfIdIsDefault()
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
                createdEntity = await _repository.CreateOrUpdateAsync(newEntity);
            }

            // Assert
            Assert.IsNotNull(createdEntity);
            Assert.IsTrue(createdEntity.ID > 0);
            Assert.AreEqual(newEntity.SampleValue, createdEntity.SampleValue);
        }

        [TestMethod]
        public async Task ReadOnlyRepository_CreateOrUpdateAsync_ShouldCreateObjectWithSpecifiedIdIfNotExistsYet()
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
                createdEntity = await _repository.CreateOrUpdateAsync(newEntity);
            }

            // Assert
            Assert.IsNotNull(createdEntity);
            Assert.AreEqual(expected: newEntity, actual: createdEntity);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadOnlyRepository_CreateOrUpdateAsync_ShouldThrowExcetionIfRepositoryIsDisposed()
        {
            // Arrange
            using (_repository)
            {
                // just for dispose
            }

            // Act
            await _repository.GetAsync();

            // Assert
        }
    }
}
