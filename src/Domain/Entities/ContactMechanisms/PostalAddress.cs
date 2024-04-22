using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Entities.Core;

namespace UtterlyComplete.Domain.Entities.ContactMechanisms
{
    public sealed class PostalAddress : ContactMechanism
    {
        [Required]
        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }
    }
}
