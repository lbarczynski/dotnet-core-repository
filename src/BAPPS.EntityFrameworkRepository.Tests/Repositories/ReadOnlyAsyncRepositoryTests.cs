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
    public class ReadOnlyAsyncRepositoryTests : RepositoryTestsBase
    {
        private IReadOnlyAsyncRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object);
        }

        [TestMethod]
        public async Task ReadOnlyAsyncRepository_GetAsync_ShouldReturnAllObjectsFromDatabase()
        {
            // Arrange
            var expected = _testData;

            // Act
            List<SampleEntity> values;
            using (_repository)
            {
                values = (await _repository.GetAsync()).ToList();
            }

            // Assert
            Assert.AreEqual(expected.Count, values.Count);
            for (var i = 0; i < values.Count; i++)
            {
                Assert.AreEqual(expected[i], values[i]);
            }

        }

        [TestMethod]
        public async Task ReadOnlyAsyncRepository_GetAsync_ShouldReturnValidObjectForSpecifiedId()
        {
            using (_repository)
            {
                // arrange
                var all = await _repository.GetAsync();
                var existingEntity = all.ToList()[0];

                // act
                var actualValue = await _repository.GetAsync(existingEntity.ID);

                // assert
                Assert.AreEqual(existingEntity, actualValue);
            }
        }

        [TestMethod]
        public async Task ReadOnlyAsyncRepository_GetAsync_ShouldReturnNullIfObjectWithSpecifiedIdNotExists()
        {
            // arrange
            long id = _testData.Max(q => q.ID) + 1;

            // act
            SampleEntity actualValue;
            using (_repository)
            {
                actualValue = await _repository.GetAsync(id);
            }

            // assert
            Assert.AreSame(expected: null, actual: actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadOnlyAsyncRepository_GetAsync_GetByIdShouldThrowExecptionIfRepositoryIsDisposed()
        {
            // Arrange
            const int id = 0;
            using (_repository)
            {
                // just for dispose
            }

            // Act
            await _repository.GetAsync(id);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public async Task ReadOnlyAsyncRepository_GetAsync_GetAllShouldThrowExecptionIfRepositoryIsDisposed()
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