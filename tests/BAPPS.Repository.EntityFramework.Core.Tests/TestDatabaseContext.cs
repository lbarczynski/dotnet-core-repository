using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BAPPS.Repository.EntityFramework.Core.Tests
{
    public class TestDatabaseContext : DbContext
    {
        public TestDatabaseContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<SampleEntity> TestEntities { get; set; }

        public static TestDatabaseContext Create()
        {
            var options = new DbContextOptionsBuilder<TestDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return new TestDatabaseContext(options);
        }

        public TestDatabaseContext WithData()
        {
            for (var i = 0; i < 1000; i++)
            {
                TestEntities.Add(new SampleEntity { SampleValue = string.Format("Test {0}", i) });
            }

            SaveChanges();
            return this;
        }
    }
}
