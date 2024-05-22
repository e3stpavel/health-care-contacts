using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;
using UtterlyComplete.IntegrationTests.Infrastructure.Data.Common;

namespace UtterlyComplete.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class RepositoryIntegrationTests : BaseRepositoryIntegrationTests
    {
        public static IEnumerable<Entity[]> ShallowEntities
        {
            get
            {
                return
                [
                    [new Party() { Id = 1 }],
                    [new AmbulatorySurgeryCenter()
                    {
                        Description = "test",
                        FacilityType = new() { Description = "test type" }
                    }],
                    [new ElectronicAddress() { Id = 119, Value = "test" }],
                    [new FacilityType() { Description = "test" }]
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ShallowEntities))]
        public async Task SaveAsync_ShouldSaveChanges(Entity entity)
        {
            using ApplicationDbContext context = DbContext;
            Repository underTest = new(context);

            await context.AddAsync(entity);

            bool hasChanges = await underTest.SaveAsync();

            Assert.IsTrue(hasChanges);
            Assert.IsNotNull(await context.FindAsync(entity.GetType(), entity.Id));
        }
    }
}
