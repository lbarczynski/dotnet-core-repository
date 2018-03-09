using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAPPS.EntityFrameworkRepository.Repositories;
using Castle.Core.Logging;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.Repositories
{
    [TestClass]
    public class ReadOnlyRepositoryTests : RepositoryTestsBase
    {
        private IReadOnlyRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new Repository<SampleEntity, long>(_databaseContext, _loggerFactoryMock.Object);
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnAllObjectsFromDatabase()
        {
            using (_repository)
            {
                // Arrange
                var expected = _testData;

                // Act
                var values = _repository.Get().ToList();

                // Assert
                Assert.AreEqual(expected.Count, values.Count);
                for (int i = 0; i < values.Count; i++)
                {
                    Assert.AreSame(expected[i], values[i]);
                }
            }
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnValidObjectForSpecifiedId()
        {
            // arrange
            const long id = 100;
            var expectedValue = _testData[(int)id];

            // act
            var actualValue = _repository.Get(id);

            // assert
            Assert.AreSame(expectedValue, actualValue);
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnNullIfObjectWithSpecifiedIdNotExists()
        {
            // arrange
            long id = _testData.Max(q => q.ID.Value) + 1;

            // act
            var actualValue = _repository.Get(id);

            // assert
            Assert.AreSame(expected: null, actual: actualValue);
        }
    }
}
