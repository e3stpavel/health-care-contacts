using System.ComponentModel.DataAnnotations;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Domain.ContactMechanisms
{
    public class TelecommunicationsNumber : ContactMechanism
    {
        // todo: pick the better types
        [Required]
        public string AreaCode { get; set; } = null!;

        [Required]
        public string ContactNumber { get; set; } = null!;

        public string? CountryCode { get; set; }
    }
}
