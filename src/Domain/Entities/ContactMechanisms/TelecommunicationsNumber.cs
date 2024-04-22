using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Entities.Core;

namespace UtterlyComplete.Domain.Entities.ContactMechanisms
{
    public sealed class TelecommunicationsNumber : ContactMechanism
    {
        // todo: pick the better types
        [Required]
        public string AreaCode { get; set; } = null!;

        [Required]
        public string ContactNumber { get; set; } = null!;

        public string? CountryCode { get; set; }
    }
}
