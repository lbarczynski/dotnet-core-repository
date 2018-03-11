using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BAPPS.EntityFrameworkRepository.Entity;

namespace BAPPS.EntityFrameworkRepository.Tests
{
    public class SampleEntity : IEntity<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string SampleValue { get; set; }

        #region IEntity

        public long GetID()
        {
            return ID;
        }

        #endregion IEntity

        public override bool Equals(object obj)
        {
            if (obj is SampleEntity toCompare)
            {
                return ID == toCompare.ID && SampleValue == toCompare.SampleValue;
            }

            return false;
        }

        public SampleEntity Clone()
        {
            return new SampleEntity()
            {
                ID = this.ID,
                SampleValue = this.SampleValue
            };
        }
    }
}
