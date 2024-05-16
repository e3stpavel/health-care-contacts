using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Infrastructure.Data.TestingUtils;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UnitTests.Infrastructure.Data.CrudRepositoryUnitTests
{
    [TestClass]
    public class CrudRepositoryQueryUnitTests
    {
        private readonly CrudRepository<Entity> _underTest;

        private readonly Mock<DbSet<Entity>> _mockDbSet;

        public CrudRepositoryQueryUnitTests()
        {
            // mock DbSet `toListAsync()`
            //  https://stackoverflow.com/a/40491640
            Mock<DbSet<Entity>> mockSet = new();
            IQueryable<Entity> entities = ShallowEntities.AsQueryable;

            mockSet
                .As<IAsyncEnumerable<Entity>>()
                .Setup(set => set.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Entity>(entities.GetEnumerator()));

            mockSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.Provider)
                .Returns(new TestAsyncQueryProvider<Entity>(entities.Provider));

            mockSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.Expression)
                .Returns(entities.Expression);

            mockSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.ElementType)
                .Returns(entities.ElementType);

            mockSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.GetEnumerator())
                .Returns(entities.GetEnumerator());

            // mock context
            DbContextOptions<ApplicationDbContext> options = new();
            Mock<ApplicationDbContext> mockContext = new(options);

            mockContext
                .Setup(context => context.Set<Entity>())
                .Returns(mockSet.Object);

            _mockDbSet = mockSet;
            _underTest = new CrudRepository<Entity>(mockContext.Object);
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntities.AsDynamicData), typeof(ShallowEntities))]
        public async Task FindByIdAsync_WhenId_ShouldReturnEntity(Entity entity)
        {
            int entityId = entity.Id;
            List<Entity> shallowEntities = ShallowEntities.AsList;

            IEnumerable<int> entityIds = shallowEntities.Select(e => e.Id);

            _mockDbSet
                .Setup(set => set.FindAsync(It.IsIn(entityIds)))
                .ReturnsAsync((object[] keyValues) =>
                {
                    int id = int.Parse(keyValues.ElementAt(0).ToString()!);

                    return shallowEntities.First(e => e.Id == id && e.GetType() == entity.GetType());
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
            IEnumerable<int> entityIds = ShallowEntities.AsList.Select(e => e.Id);

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

            CollectionAssert.AreEqual(ShallowEntities.AsList, foundEntities.ToList());
        }
    }
}
