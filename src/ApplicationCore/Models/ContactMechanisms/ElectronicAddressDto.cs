using System.Text.Json.Serialization;

namespace UtterlyComplete.ApplicationCore.Models.ContactMechanisms
{
    public record ElectronicAddressDto(
        int Id, [property: JsonPropertyName("uri")] string Value) : ContactMechanismDto(Id);
}
