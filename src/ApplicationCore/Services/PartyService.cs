using AutoMapper;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Services
{
    public class PartyService : BaseService<Party, PartyDto>
    {
        public PartyService(IMapper mapper, ICrudRepository<Party> repository) : base(mapper, repository)
        {
        }
    }
}
