using Microsoft.EntityFrameworkCore;
using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UtterlyComplete.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class RepositoryIntegrationTests
    {
        private readonly Repository _underTest;

        private readonly ApplicationDbContext _context;

        public RepositoryIntegrationTests()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(RepositoryIntegrationTests)}Database")
                .Options;

            _context = new ApplicationDbContext(options);
            _underTest = new Repository(_context);
        }

        public static IEnumerable<Entity[]> ShallowEntities
        {
            get
            {
                return
                [
                    [new Party() { Id = 1 }],
                    [new AmbulatorySurgeryCenter() { Description = "test" }],
                    [new ElectronicAddress() { Id = 119, Value = "test" }],
                    [new FacilityType() { Description = "test" }]
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntities))]
        public async Task SaveAsync_ShouldSaveChanges(Entity entity)
        {
            await _context.AddAsync(entity);

            bool hasChanges = await _underTest.SaveAsync();

            Assert.IsTrue(hasChanges);
            Assert.IsNotNull(await _context.FindAsync(entity.GetType(), entity.Id));
        }
    }
}
