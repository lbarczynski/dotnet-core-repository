using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAPPS.Repository.EntityFramework.Core.Context;
using BAPPS.Repository.EntityFramework.Core.Exceptions;
using BAPPS.Repository.EntityFramework.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BAPPS.Repository.EntityFramework.Core.Tests.Repositories
{
    [TestClass]
    public class CrudAsyncRepositoryTests : RepositoryTestsBase
    {
        private ICrudAsyncRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new TestsRepository(new DbContextAdapter(TestDatabaseContext), LoggerFactoryMock.Object);
        }

        [TestMethod]
        public async Task CrudAsyncRepository_CreateOrUpdateAsync_ShouldUpdateObjectIfFoundObjectWithTheSameId()
        {
            using (_repository)
            {
                // Arrange
                var all = await _repository.GetAsync();
                var existingObject = all.ToList().ElementAt(0);
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
        public async Task CrudAsyncRepository_CreateOrUpdateAsync_ShouldCreateObjectIfIdIsDefault()
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
        [ExpectedException(typeof(EntityFrameworkRepositoryException))]
        public async Task CrudAsyncRepository_CreateOrUpdateAsync_ShouldThrowExeceptionIfIDValueIsNotValid()
        {
            // Arrange
            var id = TestData.Max(q => q.ID) + 100;
            var newEntity = new SampleEntity()
            {
                ID = id,
                SampleValue = "new value"
            };

            // Act
            using (_repository)
            {
                await _repository.CreateOrUpdateAsync(newEntity);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task CrudAsyncRepository_CreateOrUpdateAsync_ShouldThrowExcetionIfRepositoryIsDisposed()
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


        [TestMethod]
        public async Task CrudAsyncRepository_DeleteAsync_ByIdShouldDeleteObjectIfIdExists()
        {
            // Arrange
            var all = await _repository.GetAsync();
            var id = all.ToList()[0].ID;

            // Act
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            var actualValue = await _repository.GetAsync(id);

            // Assert
            Assert.IsNull(actualValue);
        }

        [TestMethod]
        public async Task CrudAsyncRepository_DeleteAsync_ByEntityShouldDeleteObjectIfExists()
        {
            // Arrange
            var all = await _repository.GetAsync();
            var existingEntity = all.ToList()[0];
            var id = existingEntity.ID;

            // Act
            await _repository.DeleteAsync(existingEntity);
            await _repository.SaveAsync();
            var actualValue = await _repository.GetAsync(id);

            // Assert
            Assert.IsNull(actualValue);
        }


        [TestMethod]
        public async Task CrudAsyncRepository_DeleteAsync_ByIdShouldDoNothingIfIdNotExists()
        {
            // Arrange
            var all = await _repository.GetAsync();
            var notValidId = all.Max(q => q.ID) + 1;
            var expectedItemsCount = all.Count();

            // Act
            await _repository.DeleteAsync(notValidId);
            await _repository.SaveAsync();
            var actualItemsCount = (await _repository.GetAsync()).Count();

            // Assert
            Assert.AreEqual(expectedItemsCount, actualItemsCount);
        }

        [TestMethod]
        public async Task CrudAsyncRepository_DeleteAsync_ByEntityShouldDoNothingIfEntityNotExists()
        {
            // Arrange
            var notExistingEntity = new SampleEntity() { SampleValue = "not existing entity" };
            var all = await _repository.GetAsync();
            var expectedItemsCount = all.Count();

            // Act
            await _repository.DeleteAsync(notExistingEntity);
            await _repository.SaveAsync();
            var actualItemsCount = (await _repository.GetAsync()).Count();

            // Assert
            Assert.AreEqual(expectedItemsCount, actualItemsCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task CrudAsyncRepository_DeleteAsync_ByIdShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            long id;
            using (_repository)
            {
                var all = await _repository.GetAsync();
                id = all.ToList()[0].ID;
            }

            // Act
            await _repository.DeleteAsync(id);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task CrudAsyncRepository_DeleteAsync_ByEntityShouldThrowExceptionIfRepositoryIsDisposed()
        {
            // Arrange
            SampleEntity sampleEntity;
            using (_repository)
            {
                var all = await _repository.GetAsync();
                sampleEntity = all.ToList()[0];
            }

            // Act
            await _repository.DeleteAsync(sampleEntity);
        }
    }
}
