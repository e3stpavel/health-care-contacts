using Microsoft.EntityFrameworkCore;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;
using UtterlyComplete.IntegrationTests.Infrastructure.Data.Common;

namespace IntegrationTests.Infrastructure.Data
{
    [TestClass]
    public class CrudRepositoryIntegrationTests : BaseRepositoryIntegrationTests
    {
        private readonly ApplicationDbContext _context;
        
        private readonly CrudRepository<Hospital> _underTest;

        public CrudRepositoryIntegrationTests()
        {
            _context = DbContext;
            _underTest = new CrudRepository<Hospital>(_context);
        }

        private static Hospital ShallowFacility
        {
            get => new() { Description = "test facility" };
        }

        private static FacilityType ShallowFacilityType
        {
            get => new() { Description = "test type" };
        }

        private static ElectronicAddress ShallowContactMechanism
        {
            get => new() { Value = "example.com" };
        }

        [TestInitialize]
        public async Task Setup()
        {
            Facility facility = new Hospital()
            {
                Description = "initial test facility",
                FacilityType = ShallowFacilityType
            };

            await _context.AddAsync(facility);
            await _context.SaveChangesAsync();
        }

        [TestMethod]
        public async Task AddAsync_ShouldAddEntity()
        {
            Hospital hospital = ShallowFacility;
            hospital.FacilityType = ShallowFacilityType;

            Hospital createdHospital = await _underTest.AddAsync(hospital);
            await _underTest.SaveAsync();

            Assert.AreEqual(hospital, createdHospital);
            Assert.AreEqual(hospital, await _context.Facilities.FindAsync(hospital.Id));
        }

        [TestMethod]
        public async Task AddAsync_WhenInvalidState_ShouldThrowException()
        {
            Hospital hospital = ShallowFacility;

            await _underTest.AddAsync(hospital);

            await Assert.ThrowsExceptionAsync<DbUpdateException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task AddAsync_WhenHasRelatedData_ShouldAddAllEntities()
        {
            Hospital hospital = ShallowFacility;
            hospital.FacilityType = ShallowFacilityType;

            hospital.ContactMechanisms.Add(ShallowContactMechanism);

            await _underTest.AddAsync(hospital);
            await _underTest.SaveAsync();

            Facility? fetchedHospital = await _context.Facilities.FindAsync(hospital.Id);

            Assert.AreEqual(hospital, fetchedHospital);
            CollectionAssert.AreEqual(
                hospital.ContactMechanisms.ToList(), fetchedHospital?.ContactMechanisms.ToList());
        }

        [TestMethod]
        public async Task AddAsync_WhenEntityExists_ShouldThrowException()
        {
            Hospital hospital = ShallowFacility;
            hospital.Id = 1;
            hospital.FacilityType = ShallowFacilityType;

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => _underTest.AddAsync(hospital));
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenIsEntity_ShouldFindEntity()
        {
            Hospital? foundHospital = await _underTest.FindByIdAsync(1);

            StringAssert.StartsWith(foundHospital?.Description, "initial");
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenIsNotEntity_ShouldBeNull()
        {
            Hospital? foundHospital = await _underTest.FindByIdAsync(2);

            Assert.IsNull(foundHospital);
        }

        [TestMethod]
        public async Task FindByIdAsync_WhenHasRelatedData_ShouldBeAvailable()
        {
            Hospital? foundHospital = await _underTest.FindByIdAsync(1);

            Assert.AreEqual(
                ShallowFacilityType.Description, foundHospital?.FacilityType.Description);
        }

        [TestMethod]
        public async Task FindAllAsync_WhenHasEntities_ShouldFindAll()
        {
            IReadOnlyList<Hospital> foundHospitals = await _underTest.FindAllAsync();

            Assert.AreEqual(1, foundHospitals.Count);
        }

        [TestMethod]
        public async Task FindAllAsync_WhenHasNoEntities_ShouldBeEmptyList()
        {
            _context.RemoveRange(_context.Facilities);
            await _context.SaveChangesAsync();

            IReadOnlyList<Hospital> foundHospitals = await _underTest.FindAllAsync();

            Assert.AreEqual(0, foundHospitals.Count);
        }

        private async Task<Hospital> FindInitialFacility(int id = 1)
        {
            Hospital? hospital = await _context.Facilities.FindAsync(id) as Hospital;

            if (hospital is null)
                Assert.Fail($"Facility was not found!");

            return hospital;
        }

        [TestMethod]
        public async Task Update_ShouldUpdateEntity()
        {
            Hospital hospital = await FindInitialFacility();

            hospital.Description = "updated facility";

            _underTest.Update(hospital);
            await _underTest.SaveAsync();

            Assert.AreEqual(hospital, await FindInitialFacility());
        }

        [TestMethod]
        public async Task Update_WhenIsNoEntity_ShouldThrowException()
        {
            Hospital hospital = ShallowFacility;
            hospital.Id = 2; // doing that to trick ef core to believe entity exists
            await _context.AddAsync(hospital);

            hospital.Description = "updated facility";

            _underTest.Update(hospital);

            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Update_WhenHasRelatedData_ShouldUpdateAllEntities()
        {
            Hospital hospital = await FindInitialFacility();
            hospital.ContactMechanisms.Add(ShallowContactMechanism);

            _underTest.Update(hospital);
            await _underTest.SaveAsync();

            Hospital fetchedHospital = await FindInitialFacility();

            Assert.AreEqual(hospital, fetchedHospital);
            CollectionAssert.AreEqual(
                hospital.ContactMechanisms.ToList(), fetchedHospital.ContactMechanisms.ToList());
        }

        [TestMethod]
        public async Task Update_WhenIdIsUpdated_ShouldThrowException()
        {
            Hospital hospital = await FindInitialFacility();
            hospital.Id = 2;
            
            _underTest.Update(hospital);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Remove_ShouldRemoveEntity()
        {
            Hospital hospital = await FindInitialFacility();

            _underTest.Remove(hospital);
            await _underTest.SaveAsync();

            Assert.IsNull(await _context.Facilities.FindAsync(hospital.Id));
        }

        [TestMethod]
        public async Task Remove_WhenIsNoEntity_ShouldThrowException()
        {
            Hospital hospital = ShallowFacility;
            hospital.Id = 2;
            
            _underTest.Remove(hospital);

            await Assert.ThrowsExceptionAsync<DbUpdateConcurrencyException>(
                () => _underTest.SaveAsync());
        }

        [TestMethod]
        public async Task Remove_WhenHasRelatedData_ShouldRemoveEntity()
        {
            Hospital hospital = await FindInitialFacility();
            hospital.ContactMechanisms.Add(ShallowContactMechanism);
            
            _context.Update(hospital);
            await _context.SaveChangesAsync();

            _underTest.Remove(hospital);
            await _underTest.SaveAsync();

            Assert.IsNull(await _context.Facilities.FindAsync(hospital.Id));
            Assert.IsNotNull(await _context.FindAsync(typeof(ElectronicAddress), 1));
        }
    }
}