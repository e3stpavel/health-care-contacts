using UtterlyComplete.Domain.Core;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using AutoMapper;

namespace WebAPI.Controllers
{
    public class PartiesController : BaseController<Party, PartyDto>
    {
        public PartiesController(IMapper mapper, ICrudRepository<Party> repository) : base(mapper, repository)
        {
        }
    }
}
