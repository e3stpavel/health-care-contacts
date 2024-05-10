using AutoMapper;
using System.Text.Json;
using System.Text.RegularExpressions;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;

namespace UtterlyComplete.ApplicationCore.Mappings
{
    internal class FacilityProfile : Profile
    {
        public FacilityProfile()
        {
            CreateMap<Facility, FacilityDto>()
                .ForCtorParam(nameof(FacilityDto.Type),
                    opt => opt.MapFrom(src => JsonNamingPolicy.CamelCase.ConvertName(src.Type))) // convert to camelCase
                .IncludeAllDerived()
                .ReverseMap()
                .IncludeAllDerived()
                .ConstructUsing((src, context) =>
                {
                    // convert to PascalCase
                    string type = Regex.Replace(src.Type, @"\b\p{Ll}", match => match.Value.ToUpperInvariant());

                    return type switch
                    {
                        nameof(AmbulatorySurgeryCenter) => context.Mapper.Map<AmbulatorySurgeryCenter>(src),
                        nameof(Clinic) => context.Mapper.Map<Clinic>(src),
                        nameof(Floor) => context.Mapper.Map<Floor>(src),
                        nameof(Hospital) => context.Mapper.Map<Hospital>(src),
                        nameof(MedicalBuilding) => context.Mapper.Map<MedicalBuilding>(src),
                        nameof(MedicalOffice) => context.Mapper.Map<MedicalOffice>(src),
                        nameof(Room) => context.Mapper.Map<Room>(src),
                        _ => throw new NotImplementedException($"Entity type cannot be extracted or is not implemented ('{type}')"),
                    };
                })
                .ForMember(nameof(Facility.Type), opt => opt.Ignore());

            CreateMap<AmbulatorySurgeryCenter, FacilityDto>()
                .ReverseMap();

            CreateMap<Clinic, FacilityDto>()
                .ReverseMap();

            CreateMap<Floor, FacilityDto>()
                .ReverseMap();

            CreateMap<Hospital, FacilityDto>()
                .ReverseMap();

            CreateMap<MedicalBuilding, FacilityDto>()
                .ReverseMap();

            CreateMap<MedicalOffice, FacilityDto>()
                .ReverseMap();

            CreateMap<Room, FacilityDto>()
                .ReverseMap();
        }
    }
}
