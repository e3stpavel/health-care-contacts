using UtterlyComplete.ApplicationCore.Models.Common;

namespace UtterlyComplete.ApplicationCore.Models
{
    public record FacilityDto(
        int Id,
        string Type,
        string Description,
        string FacilityTypeDescription,
        int? SquareFootage,
        FacilityDto? PartOfFacility,
        IReadOnlyCollection<ContactMechanismDto> ContactMechanisms) : EntityDto(Id);
}
