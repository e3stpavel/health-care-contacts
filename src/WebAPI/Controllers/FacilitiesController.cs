using UtterlyComplete.ApplicationCore.Interfaces.Services;
using UtterlyComplete.ApplicationCore.Models;

namespace WebAPI.Controllers
{
    public class FacilitiesController : BaseController<FacilityDto>
    {
        public FacilitiesController(IService<FacilityDto> service) : base(service)
        {
        }
    }
}
