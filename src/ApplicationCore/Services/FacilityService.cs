using AutoMapper;
using Microsoft.Extensions.Logging;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Services
{
    public class FacilityService : BaseService<Facility, FacilityDto>
    {
        public FacilityService(ILoggerFactory logger, IMapper mapper, ICrudRepository<Facility> repository)
            : base(logger, mapper, repository)
        {
        }
    }
}
