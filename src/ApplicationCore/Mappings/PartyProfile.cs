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
                .ForCtorParam(nameof(PartyDto.Facilities), opt => opt.MapFrom((src, context) =>
                {
                    return src.PartyFacilities.Select(j =>
                    {
                        FacilityDto facility = context.Mapper.Map<FacilityDto>(j.Facility);

                        return new PartyFacilityDto(facility, j.FacilityRoleType.Value);
                    });
                }))
                .ReverseMap()
                .ForMember(nameof(Party.PartyFacilities),
                    opt => opt.MapFrom((src, party) =>
                    {
                        return src.Facilities.Select(facilitySrc => new PartyFacility
                        {
                            PartyId = party.Id,
                            FacilityId = facilitySrc.Id,
                            FacilityRoleTypeId = Enum.TryParse(
                                facilitySrc.RoleType, out FacilityRoleTypeId roleType) ? roleType : FacilityRoleTypeId.UNKNOWN
                        });
                    }));
        }
    }
}
