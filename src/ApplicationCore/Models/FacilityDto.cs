using System.Text.Json.Serialization;
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
        [property: JsonPropertyName("contacts")] IReadOnlyCollection<ContactMechanismDto> ContactMechanisms) : EntityDto(Id);
}
