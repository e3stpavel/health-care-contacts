using System.Text.Json.Serialization;
using UtterlyComplete.ApplicationCore.Models.Common;
using UtterlyComplete.ApplicationCore.Models.ContactMechanisms;

namespace UtterlyComplete.ApplicationCore.Models
{
    [JsonDerivedType(typeof(PostalAddressDto), typeDiscriminator: "postalAddress")]
    [JsonDerivedType(typeof(ElectronicAddressDto), typeDiscriminator: "electronicAddress")]
    [JsonDerivedType(typeof(TelecommunicationsNumberDto), typeDiscriminator: "telecommunicationsNumber")]
    public abstract record ContactMechanismDto(int Id) : EntityDto(Id);
}
