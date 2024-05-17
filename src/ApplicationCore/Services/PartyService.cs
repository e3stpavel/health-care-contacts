using AutoMapper;
using Microsoft.Extensions.Logging;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Services
{
    public class PartyService : BaseService<Party, PartyDto>
    {
        public PartyService(ILoggerFactory logger, IMapper mapper, ICrudRepository<Party> repository)
            : base(logger, mapper, repository)
        {
        }
    }
}
