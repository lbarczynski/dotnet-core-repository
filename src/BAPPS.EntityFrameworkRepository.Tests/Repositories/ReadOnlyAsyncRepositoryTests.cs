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
        public async Task ReadOnlyRepository_GetAsync_ShouldReturnAllObjectsFromDatabase()
        {
            using (_repository)
            {
                // Arrange
                var expected = _testData;

                // Act
                var values = (await _repository.GetAsync()).ToList();

                // Assert
                Assert.AreEqual(expected.Count, values.Count);
                for (var i = 0; i < values.Count; i++)
                {
                    Assert.AreSame(expected[i], values[i]);
                }
            }
        }

        [TestMethod]
        public async Task ReadOnlyRepository_GetAsync_ShouldReturnValidObjectForSpecifiedId()
        {
            // arrange
            const long id = 100;
            var expectedValue = _testData[(int)id];

            // act
            var actualValue = await _repository.GetAsync(id);

            // assert
            Assert.AreSame(expectedValue, actualValue);
        }

        [TestMethod]
        public async Task ReadOnlyRepository_GetAsync_ShouldReturnNullIfObjectWithSpecifiedIdNotExists()
        {
            // arrange
            long id = _testData.Max(q => q.ID.Value) + 1;

            // act
            var actualValue = await _repository.GetAsync(id);

            // assert
            Assert.AreSame(expected: null, actual: actualValue);
        }
    }
}
