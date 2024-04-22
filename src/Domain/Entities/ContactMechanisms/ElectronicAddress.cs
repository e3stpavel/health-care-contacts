using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Entities.Core;

namespace UtterlyComplete.Domain.Entities.ContactMechanisms
{
    public sealed class ElectronicAddress : ContactMechanism
    {
        [Required]
        [Column("ElectronicAddressString")]
        public string Value { get; set; } = null!;
    }
}
