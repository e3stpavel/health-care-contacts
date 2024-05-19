using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using UtterlyComplete.Infrastructure.Data.Contexts;

namespace UtterlyComplete.IntegrationTests.Infrastructure.Data.Common
{
    public class BaseRepositoryIntegrationTests : IDisposable
    {
        private readonly DbConnection _connection;

        private readonly DbContextOptions<ApplicationDbContext> _options;

        protected BaseRepositoryIntegrationTests()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            using ApplicationDbContext context = new(_options);
            context.Database.EnsureCreated();
        }

        protected ApplicationDbContext DbContext
        {
            get => new(_options);
        }

        public void Dispose() => _connection.Dispose();
    }
}
