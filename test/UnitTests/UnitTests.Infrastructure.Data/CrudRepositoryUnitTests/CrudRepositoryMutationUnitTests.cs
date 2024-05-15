using Microsoft.EntityFrameworkCore;
using Moq;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UnitTests.Infrastructure.Data.CrudRepositoryUnitTests
{
    [TestClass]
    public class CrudRepositoryMutationUnitTests
    {
        private readonly CrudRepository<Entity> _underTest;

        private readonly Mock<DbSet<Entity>> _mockDbSet;

        public CrudRepositoryMutationUnitTests()
        {
            DbContextOptions<ApplicationDbContext> options = new();
            Mock<DbSet<Entity>> mockSet = new();
            Mock<ApplicationDbContext> mockContext = new(options);

            mockContext
                .Setup(context => context.Set<Entity>())
                .Returns(mockSet.Object);

            _mockDbSet = mockSet;
            _underTest = new CrudRepository<Entity>(mockContext.Object);
        }

        public static IEnumerable<Entity[]> ShallowEntities
        {
            get
            {
                return
                [
                    [new Party() { Id = 1 }],
                    [new Facility() { Id = 10 }],
                    [new AmbulatorySurgeryCenter() { Id = 3 }]
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntities))]
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
        [DynamicData(nameof(ShallowEntities))]
        public void Update_ShouldUpdateEntity(Entity entity)
        {
            _mockDbSet
                .Setup(set => set.Update(It.IsAny<Entity>()));

            _underTest.Update(entity);

            _mockDbSet
                .Verify(set => set.Update(entity), Times.Once());
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntities))]
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
