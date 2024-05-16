using Microsoft.EntityFrameworkCore;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UtterlyComplete.IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class CrudRepositoryIntegrationTests
    {
        private readonly CrudRepository<Hospital> _underTest;

        private readonly ApplicationDbContext _context;

        public CrudRepositoryIntegrationTests()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(CrudRepositoryIntegrationTests)}Database")
                .Options;

            _context = new ApplicationDbContext(options);
            _underTest = new CrudRepository<Hospital>(_context);
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddEntity()
        {
            Hospital hospital = new()
            {
                Description = "test facility",
            };

            Hospital createdHospital = await _underTest.AddAsync(hospital);
            await _underTest.SaveAsync();

            Assert.AreEqual(hospital, createdHospital);
            Assert.AreEqual(hospital, await _context.Facilities.FindAsync(hospital.Id));
        }

        [TestMethod]
        public async Task AddAsync_WhenInvalidState_ShouldThrowException()
        {
            Hospital hospital = new();

            await _underTest.AddAsync(hospital);

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task AddAsync_WhenHasRelatedData_ShouldAddAllEntities()
        {
            FacilityType type = new() { Description = "test type" };

            Hospital hospital = new()
            {
                Description = "test facility",
                FacilityType = type
            };

            await _underTest.AddAsync(hospital);
            await _underTest.SaveAsync();

            Hospital? fetchedHospital = await _context.Facilities.FindAsync(hospital.Id) as Hospital;
            
            Assert.AreEqual(type, fetchedHospital?.FacilityType);
            Assert.AreEqual(type.Id, fetchedHospital?.FacilityTypeId);
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenIsEntity_ShouldFindEntity()
        {
            Hospital hospital = new()
            {
                Description = "test facility",
            };

            await _context.AddAsync(hospital);

            Hospital? foundHospital = await _underTest.FindByIdAsync(hospital.Id);

            Assert.AreEqual(hospital, foundHospital);
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenIsNotEntity_ShouldBeNull()
        {
            Hospital? foundHospital = await _underTest.FindByIdAsync(int.MaxValue);

            Assert.IsNull(foundHospital);
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenHasRelatedData_ShouldBeAvailable()
        {
            FacilityType type = new() { Description = "test type" };

            Hospital hospital = new()
            {
                Description = "test facility",
                FacilityType = type
            };

            await _context.AddAsync(hospital);

            Hospital? foundHospital = await _underTest.FindByIdAsync(hospital.Id);

            Assert.AreEqual(type, foundHospital?.FacilityType);
        }

        [TestMethod]
        public async Task FindAllAsync_WhenHasEntities_ShouldFindAll()
        {
            // todo: put inside [TestCleanup]?
            // remove the existing data
            _context.RemoveRange([.. await _context.Facilities.ToListAsync()]);

            List<Hospital> hospitals = [];
            
            for (int i = 0; i <= 10; i++)
                hospitals.Add(new Hospital() { Description = $"test facility {i}" });

            await _context.AddRangeAsync(hospitals);
            await _context.SaveChangesAsync();

            IReadOnlyList<Hospital> foundHospitals = await _underTest.FindAllAsync();

            CollectionAssert.AreEqual(hospitals, foundHospitals.ToList());
        }

        [TestMethod]
        public async Task FindAllAsync_WhenHasNoEntities_ShouldBeEmptyList()
        {
            _context.RemoveRange([.. await _context.Facilities.ToListAsync()]);
            await _context.SaveChangesAsync();

            IReadOnlyList<Hospital> foundHospitals = await _underTest.FindAllAsync();
            
            Assert.AreEqual(0, foundHospitals.Count);
        }

        // todo: Update and Delete tests
    }
}
