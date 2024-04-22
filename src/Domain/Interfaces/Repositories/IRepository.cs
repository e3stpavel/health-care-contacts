using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Interfaces.Repositories
{
    public interface IRepository
    {
        Task<bool> SaveAsync();
    }
}
