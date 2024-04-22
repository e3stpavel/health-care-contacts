using UtterlyComplete.Domain.Entities.Core;
using UtterlyComplete.Domain.Interfaces.Repositories;

namespace WebAPI.Controllers
{
    public class PartiesController : BaseController<Party>
    {
        public PartiesController(ICrudRepository<Party> repository) : base(repository)
        {
        }
    }
}
