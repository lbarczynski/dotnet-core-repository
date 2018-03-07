using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
