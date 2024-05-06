using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    public class Party : Entity
    {
        public virtual ICollection<ContactMechanism> ContactMechanisms { get; } = [];

        public virtual ICollection<PartyContactMechanism> PartyContactMechanisms { get; } = [];

        public virtual ICollection<Facility> Facilities { get; } = [];

        public virtual ICollection<PartyFacility> PartyFacilities { get; } = [];
    }
}
