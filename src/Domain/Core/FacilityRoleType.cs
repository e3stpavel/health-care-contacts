using System.ComponentModel.DataAnnotations;

namespace UtterlyComplete.Domain.Core
{
    public enum FacilityRoleTypeId : int
    {
        UNKNOWN,
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
