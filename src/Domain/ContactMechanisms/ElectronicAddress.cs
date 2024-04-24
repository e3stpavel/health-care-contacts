using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Domain.ContactMechanisms
{
    public sealed class ElectronicAddress : ContactMechanism
    {
        [Required]
        [Column("ElectronicAddressString")]
        public string Value { get; set; } = null!;
    }
}
