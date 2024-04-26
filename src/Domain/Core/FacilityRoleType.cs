using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtterlyComplete.Domain.Core
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
        public string Value { get; set; } = null!;
    }
}
