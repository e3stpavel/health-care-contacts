using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Infrastructure.Data.TestingUtils;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UnitTests.Infrastructure.Data.CrudRepositoryUnitTests
{
    [TestClass]
    public class CrudRepositoryQueryUnitTests
    {
        private readonly CrudRepository<Entity> _underTest;

        private readonly Mock<DbSet<Entity>> _mockDbSet;

        private static readonly List<Entity> _shallowEntities =
        [
            new Party() { Id = 27 },
            new Floor() { Id = 5 },
            new ElectronicAddress() { Id = 1 }
        ];

        public CrudRepositoryQueryUnitTests()
        {
            // mock DbSet `toListAsync()`
            //  https://stackoverflow.com/a/40491640
            Mock<DbSet<Entity>> mockSet = new();
            IQueryable<Entity> entities = _shallowEntities.AsQueryable();

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
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(27)]
        public async Task FindByIdAsync_WhenId_ShouldReturnEntity(int entityId)
        {
            IEnumerable<int> entityIds = _shallowEntities.Select(e => e.Id);

            _mockDbSet
                .Setup(set => set.FindAsync(new object[] { It.IsIn(entityIds) }))
                .ReturnsAsync((object[] keyValues) =>
                {
                    int id = int.Parse(keyValues.ElementAt(0).ToString()!);

                    return _shallowEntities.First(e => e.Id == id);
                });

            Entity? foundEntity = await _underTest.FindByIdAsync(entityId);
            Entity entity = _shallowEntities.First(e => e.Id == entityId);

            Assert.AreEqual(entity, foundEntity);
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
