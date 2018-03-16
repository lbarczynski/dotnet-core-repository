using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BAPPS.EntityFrameworkRepository.DbSet;
using Moq;

namespace BAPPS.EntityFrameworkRepository.Tests.TestHelpers
{
    public class MockHelpers
    {
        public static Mock<IDbSet<SampleEntity, long>> GetDbSetMock(IList<SampleEntity> dataEntities)
        {
            var mock = new Mock<IDbSet<SampleEntity, long>>();

            long id = 1;
            foreach (var entity in dataEntities)
            {
                entity.ID = id++;
            }

            mock.Setup(q => q.Add(It.IsAny<SampleEntity>()))
                .Callback<SampleEntity>(q => Add(dataEntities, q))
                .Returns<SampleEntity>(q => Add(dataEntities, q, false));

            mock.Setup(q => q.AddAsync(It.IsAny<SampleEntity>(), It.IsAny<CancellationToken>()))
                .Callback<SampleEntity, CancellationToken>((e, ct) => Add(dataEntities, e))
                .Returns<SampleEntity, CancellationToken>((e, ct) => Task.FromResult(Add(dataEntities, e, false)));

            mock.Setup(q => q.Update(It.IsAny<SampleEntity>()))
                .Callback<SampleEntity>(q => Update(dataEntities, q))
                .Returns<SampleEntity>(q => Update(dataEntities, q, false));

            mock.Setup(q => q.Remove(It.IsAny<SampleEntity>()))
                .Callback<SampleEntity>(q => Remove(dataEntities, q))
                .Returns<SampleEntity>(q => Remove(dataEntities, q, false));

            mock.Setup(q => q.AsQueryable()).Returns(dataEntities.AsQueryable());

            mock.Setup(q => q.Find(It.IsAny<long>())).Returns<long>(q => Find(dataEntities, q));

            mock.Setup(q => q.FindAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns<long, CancellationToken>((findId, ct) => Task.FromResult(Find(dataEntities, findId)));

            return mock;
        }

        private static SampleEntity Find(IList<SampleEntity> dataEntities, long id)
        {
            return dataEntities.FirstOrDefault(q => q.ID == id);
        }

        private static SampleEntity Add(IList<SampleEntity> dataEntities, SampleEntity entity, bool add = true)
        {
            if (entity.ID == 0 || !dataEntities.Any(q => q.ID == entity.ID) || !add)
            {
                entity.ID = dataEntities.Max(q => q.ID) + 1;
                if (add) dataEntities.Add(entity);
                return entity;
            }

            throw new Exception("Unable to add object - entity with ID already exists");
        }

        private static SampleEntity Update(IList<SampleEntity> dataEntities, SampleEntity entity, bool update = true)
        {
            if (entity.ID != 0 && dataEntities.Any(q => q.ID == entity.ID) || !update)
            {
                var existing = dataEntities.First(q => q.ID == entity.ID);
                var index = dataEntities.IndexOf(existing);
                if (update) dataEntities[index] = entity;
                return entity;
            }

            throw new Exception(string.Format("Unable to update object - entity with ID {0} not found!", entity.ID));
        }

        private static SampleEntity Remove(IList<SampleEntity> dataEntities, SampleEntity entity, bool remove = true)
        {
            if (entity.ID != 0 && dataEntities.Any(q => q.ID == entity.ID) || !remove)
            {
                if (remove)
                {
                    var existing = dataEntities.First(q => q.ID == entity.ID);
                    var index = dataEntities.IndexOf(existing);
                    dataEntities.RemoveAt(index);
                }
                return entity;
            }

            throw new Exception(String.Format("Unable to delete entity - entity with ID {0} not found!", entity.ID));
        }
    }
}
