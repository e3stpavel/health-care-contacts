using System.ComponentModel.DataAnnotations.Schema;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    [Table("ContactMechanisms")]
    public abstract class ContactMechanism : Entity
    {
        public virtual ICollection<Party> Parties { get; } = [];

        public virtual ICollection<Facility> Facilities { get; } = [];

        public virtual ICollection<PartyContactMechanism> PartyContactMechanisms { get; } = [];
    }
}
