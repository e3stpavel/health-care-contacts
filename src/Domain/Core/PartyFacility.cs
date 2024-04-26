using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Core
{
    public class PartyFacility
    {
        public int PartyId { get; set; }

        public int FacilityId { get; set; }

        public FacilityRoleTypeId FacilityRoleTypeId { get; set; }

        public FacilityRoleType FacilityRoleType { get; set; } = null!;
    }
}
