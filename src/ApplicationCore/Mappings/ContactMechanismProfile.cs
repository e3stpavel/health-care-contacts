using AutoMapper;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.ApplicationCore.Models.ContactMechanisms;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.ApplicationCore.Mappings
{
    internal class ContactMechanismProfile : Profile
    {
        public ContactMechanismProfile()
        {
            CreateMap<ContactMechanism, ContactMechanismDto>()
                .IncludeAllDerived()
                .ReverseMap()
                .IncludeAllDerived();

            CreateMap<PostalAddress, PostalAddressDto>()
                .ReverseMap();

            CreateMap<ElectronicAddress, ElectronicAddressDto>()
                .ReverseMap();

            CreateMap<TelecommunicationsNumber, TelecommunicationsNumberDto>()
                .ReverseMap();
        }
    }
}
