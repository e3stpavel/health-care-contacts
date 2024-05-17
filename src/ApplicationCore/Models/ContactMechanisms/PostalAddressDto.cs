namespace UtterlyComplete.ApplicationCore.Models.ContactMechanisms
{
    public record PostalAddressDto(
        int Id,
        string AddressLine1,
        string? AddressLine2) : ContactMechanismDto(Id);
}
