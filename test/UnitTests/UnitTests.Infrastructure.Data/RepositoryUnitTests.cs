using Microsoft.EntityFrameworkCore;
using Moq;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UnitTests.Infrastructure.Data
{
    [TestClass]
    public class RepositoryUnitTests
    {
        private readonly Repository _underTest;

        private readonly Mock<ApplicationDbContext> _mockDbContext;

        public RepositoryUnitTests()
        {
            DbContextOptions<ApplicationDbContext> options = new();
            Mock<ApplicationDbContext> mockContext = new(options);

            _mockDbContext = mockContext;
            _underTest = new Repository(mockContext.Object);
        }

        [TestMethod]
        public async Task SaveAsync_WhenNoChanges_ItShouldBeFalse()
        {
            _mockDbContext
                .Setup(mock => mock.SaveChangesAsync(default))
                .ReturnsAsync(0);

            Assert.IsFalse(await _underTest.SaveAsync());
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public async Task SaveAsync_WhenChanges_ItShouldBeTrue(int numberOfChanges)
        {
            _mockDbContext
                .Setup(mock => mock.SaveChangesAsync(default))
                .ReturnsAsync(numberOfChanges);

            Assert.IsTrue(await _underTest.SaveAsync());
        }
    }
}
