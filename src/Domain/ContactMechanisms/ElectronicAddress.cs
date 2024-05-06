using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Domain.ContactMechanisms
{
    public class ElectronicAddress : ContactMechanism
    {
        [Required]
        [Column("ElectronicAddressString")]
        public string Value { get; set; } = null!;
    }
}
