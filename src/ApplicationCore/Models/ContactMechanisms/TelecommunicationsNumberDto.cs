namespace UtterlyComplete.ApplicationCore.Models.ContactMechanisms
{
    public record TelecommunicationsNumberDto(
        int Id,
        string AreaCode,
        string ContactNumber,
        string? CountryCode) : ContactMechanismDto(Id);
}
