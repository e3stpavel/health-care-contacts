using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Entities.Core
{
    public enum FacilityRoleTypeId : int
    {
        OWNED,
        LEASED,
        BOOKED
    }

    public class FacilityRoleType
    {
        public FacilityRoleTypeId Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        // todo: navigation property to join entity
        //public ICollection<> PartyFacilities { get; } = [];
    }
}
