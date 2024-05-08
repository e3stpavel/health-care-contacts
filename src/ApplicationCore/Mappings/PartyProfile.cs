using AutoMapper;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Mappings
{
    internal class PartyProfile : Profile
    {
        public PartyProfile()
        {
            CreateMap<Party, PartyDto>()
                .ReverseMap();
        }
    }
}
