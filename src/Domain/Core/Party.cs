using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Common;

namespace UtterlyComplete.Domain.Core
{
    public class Party : Entity
    {
        public ICollection<ContactMechanism> ContactMechanisms { get; } = [];

        public ICollection<PartyContactMechanism> PartyContactMechanisms { get; } = [];

        public ICollection<Facility> Facilities { get; } = [];

        public ICollection<PartyFacility> PartyFacilities { get; } = [];
    }
}
