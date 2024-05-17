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

        private static Hospital ShallowHospital
        {
            get => new() { Description = "test facility" };
        }

        private static FacilityType ShallowFacilityType
        {
            get => new() { Description = "test type" };
        }

        public CrudRepositoryIntegrationTests()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(CrudRepositoryIntegrationTests)}Database")
                .Options;

            _context = new ApplicationDbContext(options);
            _underTest = new CrudRepository<Hospital>(_context);
        }

        [TestCleanup]
        public async Task Teardown()
        {
            // todo: maybe we should use sqlite in memory db and
            //  utilize transactions to avoid manually deleting all the data
            _context.ChangeTracker.Clear();
            _context.RemoveRange(_context.Facilities);
            await _context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddEntity()
        {
            Hospital hospital = ShallowHospital;
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
            FacilityType type = ShallowFacilityType;
            Hospital hospital = ShallowHospital;

            hospital.FacilityType = type;

            await _underTest.AddAsync(hospital);
            await _underTest.SaveAsync();

            Hospital? fetchedHospital = await _context.Facilities.FindAsync(hospital.Id) as Hospital;

            Assert.AreEqual(type, fetchedHospital?.FacilityType);
            Assert.AreEqual(type.Id, fetchedHospital?.FacilityTypeId);
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenIsEntity_ShouldFindEntity()
        {
            Hospital hospital = ShallowHospital;
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
            FacilityType type = ShallowFacilityType;
            Hospital hospital = ShallowHospital;

            hospital.FacilityType = type;

            await _context.AddAsync(hospital);

            Hospital? foundHospital = await _underTest.FindByIdAsync(hospital.Id);

            Assert.AreEqual(type, foundHospital?.FacilityType);
        }

        [TestMethod]
        public async Task FindAllAsync_WhenHasEntities_ShouldFindAll()
        {
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
            IReadOnlyList<Hospital> foundHospitals = await _underTest.FindAllAsync();
            
            Assert.AreEqual(0, foundHospitals.Count);
        }

        [TestMethod]
        public async Task Update_ShouldUpdateEntity()
        {
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            hospital.Description = "updated facility";

            _underTest.Update(hospital);
            await _underTest.SaveAsync();

            Assert.AreEqual(hospital, await _underTest.FindByIdAsync(hospital.Id));
        }

        [TestMethod]
        public async Task Update_WhenIsNoEntity_ShouldThrowException()
        {
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(hospital);

            hospital.Description = "updated facility";

            _underTest.Update(hospital);

            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Update_WhenNoChanges_ShouldNotUpdateEntity()
        {
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            _underTest.Update(hospital);
            await _underTest.SaveAsync();

            Assert.AreEqual(hospital, await _context.Facilities.FindAsync(hospital.Id));
        }

        [TestMethod]
        public async Task Update_WhenHasRelatedData_ShouldUpdateAllEntities()
        {
            FacilityType type = ShallowFacilityType;
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(type);
            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            hospital.FacilityType = type;

            _underTest.Update(hospital);
            await _underTest.SaveAsync();

            Hospital? fetchedHospital = await _context.Facilities.FindAsync(hospital.Id) as Hospital;

            Assert.AreEqual(hospital, fetchedHospital);
            Assert.AreEqual(type, fetchedHospital?.FacilityType);
            Assert.AreEqual(type.Id, fetchedHospital?.FacilityTypeId);
        }

        [TestMethod]
        public async Task Update_WhenIdIsUpdated_ShouldThrowException()
        {
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            hospital.Id = 500;

            _underTest.Update(hospital);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Remove_ShouldRemoveEntity()
        {
            Hospital hospital = ShallowHospital;

            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            _underTest.Remove(hospital);
            await _underTest.SaveAsync();

            Assert.IsNull(await _context.Facilities.FindAsync(hospital.Id));
        }

        [TestMethod]
        public async Task Remove_WhenIsNoEntity_ShouldThrowException()
        {
            _underTest.Remove(ShallowHospital);

            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Remove_WhenHasRelationToOne_ShouldRemoveEntity()
        {
            FacilityType type = ShallowFacilityType;
            Hospital hospital = ShallowHospital;

            hospital.FacilityType = type;

            await _context.AddAsync(hospital);
            await _context.SaveChangesAsync();

            _underTest.Remove(hospital);
            await _underTest.SaveAsync();

            Assert.IsNull(await _context.Facilities.FindAsync(hospital.Id));
            Assert.IsNotNull(await _context.FindAsync(typeof(FacilityType), type.Id));
        }

        [TestMethod]
        public async Task Remove_WhenHasRelationToMany_ShouldRemoveEntity()
        {
            Hospital hospital = ShallowHospital;

            Floor floor = new()
            {
                Description = "test partOf facility",
                PartOfFacility = hospital
            };

            await _context.AddRangeAsync(hospital, floor);
            await _context.SaveChangesAsync();

            _underTest.Remove(hospital);
            await _underTest.SaveAsync();

            Assert.IsNull(await _context.Facilities.FindAsync(hospital.Id));

            Floor? fetchedFloor = await _context.Facilities.FindAsync(floor.Id) as Floor;

            Assert.IsNull(fetchedFloor?.PartOfFacility);
            Assert.IsNull(fetchedFloor?.PartOfFacilityId);
        }
    }
}
