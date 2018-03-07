using System;
using System.Collections.Generic;
using System.Text;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository.Tests
{
    public class SampleEntity : IEntityIdProvider<long>
    {
        public long? ID { get; set; }
        public string SampleValue { get; set; }

        #region IEntityIdProvider

        public long? GetID()
        {
            return ID;
        }

        #endregion IEntityIdProvider
    }
}
