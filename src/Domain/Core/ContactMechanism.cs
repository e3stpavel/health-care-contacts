using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    [Table("ContactMechanisms")]
    public abstract class ContactMechanism : Entity
    {
        public ICollection<Party> Parties { get; } = [];

        public ICollection<PartyContactMechanism> PartyContactMechanisms { get; } = [];
    }
}
