using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.Infrastructure.Data.Contexts;

namespace UtterlyComplete.Infrastructure.Data.Repositories
{
    internal class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }
    }
}
