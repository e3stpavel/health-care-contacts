﻿using Moq;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Infrastructure.Data.Repositories;
using UtterlyComplete.UnitTests.Infrastructure.Data.Common;

namespace UtterlyComplete.UnitTests.Infrastructure.Data.CrudRepositoryUnitTests
{
    [TestClass]
    public class CrudRepositoryQueryUnitTests : BaseRepositoryUnitTests
    {
        private readonly CrudRepository<Entity> _underTest;

        public CrudRepositoryQueryUnitTests()
        {
            _underTest = new CrudRepository<Entity>(_mockDbContext.Object);
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntitiesDynamicData), typeof(BaseRepositoryUnitTests))]
        public async Task FindByIdAsync_WhenId_ShouldReturnEntity(Entity entity)
        {
            int entityId = entity.Id;

            IEnumerable<int> entityIds = _shallowEntities.Select(e => e.Id);

            _mockDbSet
                .Setup(set => set.FindAsync(It.IsIn(entityIds)))
                .ReturnsAsync((object[] keyValues) =>
                {
                    int id = int.Parse(keyValues.ElementAt(0).ToString()!);

                    return _shallowEntities.First(e => e.Id == id && e.GetType() == entity.GetType());
                });

            Entity? foundEntity = await _underTest.FindByIdAsync(entityId);

            Assert.IsInstanceOfType(foundEntity, entity.GetType());
            Assert.AreEqual(entity.Id, foundEntity.Id);
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(500)]
        [DataRow(0)]
        public async Task FindByIdAsync_WhenIdIsNotInSet_ShouldBeNull(int entityId)
        {
            IEnumerable<int> entityIds = _shallowEntities.Select(e => e.Id);

            _mockDbSet
                .Setup(set => set.FindAsync(It.IsNotIn(entityIds)))
                .ReturnsAsync(() => null);

            Entity? foundEntity = await _underTest.FindByIdAsync(entityId);

            Assert.IsNull(foundEntity);
        }

        [TestMethod]
        public async Task FindAllAsync_ShouldBeListOfEntities()
        {
            IReadOnlyList<Entity> foundEntities = await _underTest.FindAllAsync();

            CollectionAssert.AreEqual(_shallowEntities, foundEntities.ToList());
        }
    }
}
