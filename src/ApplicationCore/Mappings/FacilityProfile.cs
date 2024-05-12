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

                    Type someFacilityType = typeof(AmbulatorySurgeryCenter);

                    IEnumerable<Type> facilityTypes = someFacilityType.Assembly.GetTypes()
                        .Where(type => type.Namespace == someFacilityType.Namespace && type.IsClass && type.IsSubclassOf(typeof(Facility)));

                    Type facilityType = facilityTypes.FirstOrDefault(facilityType => type == facilityType.Name)
                        ?? throw new NotImplementedException($"Entity type cannot be extracted or is not implemented ('{type}')");
                    
                    return (Facility)context.Mapper.Map(src, src.GetType(), facilityType);
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
