﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAPPS.Repository.EntityFramework.Core.Context;
using BAPPS.Repository.EntityFramework.Core.Repositories;
using Castle.Core.Logging;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BAPPS.Repository.EntityFramework.Core.Tests.Repositories
{
    [TestClass]
    public class ReadOnlyRepositoryTests : RepositoryTestsBase
    {
        private IReadOnlyRepository<SampleEntity, long> _repository;

        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _repository = new TestsRepository(new DbContextAdapter(TestDatabaseContext), LoggerFactoryMock.Object);
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnAllObjectsFromDatabase()
        {
            // Arrange
            var expected = TestData;

            // Act
            List<SampleEntity> values;
            using (_repository)
            {
                values = _repository.Get().ToList();
            }

            // Assert
            Assert.AreEqual(expected.Count, values.Count);
            for (int i = 0; i < values.Count; i++)
            {
                Assert.AreEqual(expected[i], values[i]);
            }
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnValidObjectForSpecifiedId()
        {
            using (_repository)
            {
                // arrange
                var all = _repository.Get();
                var existingEntity = all.ToList()[0];

                // act
                var actualValue = _repository.Get(existingEntity.ID);

                // assert
                Assert.AreEqual(existingEntity, actualValue);
            }
        }

        [TestMethod]
        public void ReadOnlyRepository_Get_ShouldReturnNullIfObjectWithSpecifiedIdNotExists()
        {
            // arrange
            long id = TestData.Max(q => q.ID) + 1;

            // act
            var actualValue = _repository.Get(id);

            // assert
            Assert.AreSame(expected: null, actual: actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadOnlyRepository_Get_GetByIdShouldThrowExecptionIfRepositoryIsDisposed()
        {
            // Arrange
            const int id = 0;
            using (_repository)
            {
                // just for dispose
            }

            // Act
            _repository.Get(id);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectDisposedException))]
        public void ReadOnlyRepository_Get_GetAllShouldThrowExecptionIfRepositoryIsDisposed()
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
