using Moq;
using UtterlyComplete.Infrastructure.Data.Repositories;
using UtterlyComplete.UnitTests.Infrastructure.Data.Common;

namespace UtterlyComplete.UnitTests.Infrastructure.Data
{
    [TestClass]
    public class RepositoryUnitTests : BaseRepositoryUnitTests
    {
        private readonly Repository _underTest;

        public RepositoryUnitTests() : base()
        {
            _underTest = new Repository(_mockDbContext.Object);
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
