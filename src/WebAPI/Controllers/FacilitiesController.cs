using AutoMapper;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace WebAPI.Controllers
{
    public class FacilitiesController : BaseController<Facility, FacilityDto>
    {
        public FacilitiesController(IMapper mapper, ICrudRepository<Facility> repository) : base(mapper, repository)
        {
        }
    }
}
