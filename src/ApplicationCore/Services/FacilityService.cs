using AutoMapper;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Services
{
    public class FacilityService : BaseService<Facility, FacilityDto>
    {
        public FacilityService(IMapper mapper, ICrudRepository<Facility> repository) : base(mapper, repository)
        {
        }
    }
}
