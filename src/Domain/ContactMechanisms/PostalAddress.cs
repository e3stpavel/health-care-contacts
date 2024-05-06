using System.ComponentModel.DataAnnotations;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Domain.ContactMechanisms
{
    public class PostalAddress : ContactMechanism
    {
        [Required]
        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }
    }
}
