using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTests.Infrastructure.Data.Mocks;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;

namespace UnitTests.Infrastructure.Data.Common
{
    public class BaseRepositoryUnitTests
    {
        protected readonly Mock<DbSet<Entity>> _mockDbSet;

        protected readonly Mock<ApplicationDbContext> _mockDbContext;

        protected static readonly List<Entity> _shallowEntities =
        [
            new Party() { Id = 1 },
            new ElectronicAddress() { Id = 10 },
            new AmbulatorySurgeryCenter() { Id = 3 },
            new Floor() { Id = 5 },
            new TelecommunicationsNumber() { Id = 100 },
        ];

        protected static IEnumerable<Entity[]> ShallowEntitiesDynamicData
        {
            get => _shallowEntities.Select(e => new Entity[] { e });
        }

        protected BaseRepositoryUnitTests()
        {
            // mock DbSet `toListAsync()`
            //  https://stackoverflow.com/a/40491640
            Mock<DbSet<Entity>> mockDbSet = new();
            IQueryable<Entity> entities = _shallowEntities.AsQueryable();

            mockDbSet
                .As<IAsyncEnumerable<Entity>>()
                .Setup(set => set.GetAsyncEnumerator(default))
                .Returns(new MockAsyncEnumerator<Entity>(entities.GetEnumerator()));

            mockDbSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.Provider)
                .Returns(new MockAsyncQueryProvider<Entity>(entities.Provider));

            mockDbSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.Expression)
                .Returns(entities.Expression);

            mockDbSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.ElementType)
                .Returns(entities.ElementType);

            mockDbSet
                .As<IQueryable<Entity>>()
                .Setup(set => set.GetEnumerator())
                .Returns(entities.GetEnumerator());

            // mock context
            DbContextOptions<ApplicationDbContext> options = new();
            Mock<ApplicationDbContext> mockDbContext = new(options);
            
            mockDbContext
                .Setup(context => context.Set<Entity>())
                .Returns(mockDbSet.Object);

            _mockDbSet = mockDbSet;
            _mockDbContext = mockDbContext;
        }
    }
}
