using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Interfaces.Repositories
{
    public interface ICrudRepository<T> : IRepository where T : class
    {
        Task<T> AddAsync(T entity);

        Task<T?> FindByIdAsync(int id);

        Task<IReadOnlyList<T>> FindAllAsync();

        void Update(T entity);

        void Remove(T entity);

        //void DeleteById(int id);
    }
}
