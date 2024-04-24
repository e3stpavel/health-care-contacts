using UtterlyComplete.Domain.Core;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;

namespace WebAPI.Controllers
{
    public class PartiesController : BaseController<Party>
    {
        public PartiesController(ICrudRepository<Party> repository) : base(repository)
        {
        }
    }
}
