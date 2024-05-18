using Moq;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Infrastructure.Data.Repositories;
using UnitTests.Infrastructure.Data.Common;

namespace UnitTests.Infrastructure.Data.CrudRepositoryUnitTests
{
    [TestClass]
    public class CrudRepositoryMutationUnitTests : BaseRepositoryUnitTests
    {
        private readonly CrudRepository<Entity> _underTest;

        public CrudRepositoryMutationUnitTests() : base()
        {
            _underTest = new CrudRepository<Entity>(_mockDbContext.Object);
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntitiesDynamicData), typeof(BaseRepositoryUnitTests))]
        public async Task AddAsync_ShouldAddEntity(Entity entity)
        {
            List<Entity> entities = [];

            _mockDbSet
                .Setup(set => set.AddAsync(It.IsAny<Entity>(), default))
                .Callback((Entity e, CancellationToken _) => entities.Add(e));

            Entity createdEntity = await _underTest.AddAsync(entity);

            Assert.AreEqual(entity, createdEntity);

            Assert.AreEqual(1, entities.Count);
            CollectionAssert.Contains(entities, createdEntity);

            _mockDbSet
                .Verify(set => set.AddAsync(entity, default), Times.Once());
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntitiesDynamicData), typeof(BaseRepositoryUnitTests))]
        public void Update_ShouldUpdateEntity(Entity entity)
        {
            _mockDbSet
                .Setup(set => set.Update(It.IsAny<Entity>()));

            _underTest.Update(entity);

            _mockDbSet
                .Verify(set => set.Update(entity), Times.Once());
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntitiesDynamicData), typeof(BaseRepositoryUnitTests))]
        public void Delete_ShouldDeleteEntity(Entity entity)
        {
            _mockDbSet
                .Setup(set => set.Remove(It.IsAny<Entity>()));

            _underTest.Remove(entity);

            _mockDbSet
                .Verify(set => set.Remove(entity), Times.Once());
        }
    }
}
