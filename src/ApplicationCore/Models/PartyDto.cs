using System.Text.Json.Serialization;
using UtterlyComplete.ApplicationCore.Models.Common;

namespace UtterlyComplete.ApplicationCore.Models
{
    public record PartyDto(
        int Id,
        [property: JsonPropertyName("contacts")] IReadOnlyCollection<ContactMechanismDto> ContactMechanisms,
        IReadOnlyCollection<PartyFacilityDto> Facilities) : EntityDto(Id);
}
