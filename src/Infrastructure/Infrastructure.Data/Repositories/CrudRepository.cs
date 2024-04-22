using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Interfaces.Repositories;
using UtterlyComplete.Infrastructure.Data.Contexts;

namespace UtterlyComplete.Infrastructure.Data.Repositories
{
    internal class CrudRepository<T> : Repository, ICrudRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public CrudRepository(ApplicationDbContext context) : base(context)
        {
            _entities = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }
    }
}
