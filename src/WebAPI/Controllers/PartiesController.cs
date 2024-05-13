using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.ApplicationCore.Interfaces.Services;

namespace WebAPI.Controllers
{
    public class PartiesController : BaseController<PartyDto>
    {
        public PartiesController(IService<PartyDto> service) : base(service)
        {
        }
    }
}
