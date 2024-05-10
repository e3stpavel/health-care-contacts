using UtterlyComplete.ApplicationCore.Models.Common;

namespace UtterlyComplete.ApplicationCore.Models
{
    public record PartyDto(
        int Id,
        IReadOnlyCollection<ContactMechanismDto> ContactMechanisms,
        IReadOnlyCollection<FacilityDto> Facilities) : EntityDto(Id);
}
