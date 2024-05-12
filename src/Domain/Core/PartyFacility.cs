namespace UtterlyComplete.Domain.Core
{
    public class PartyFacility
    {
        public int PartyId { get; set; }

        public Party Party { get; set; } = null!;

        public int FacilityId { get; set; }

        public Facility Facility { get; set; } = null!;

        public FacilityRoleTypeId FacilityRoleTypeId { get; set; }

        public virtual FacilityRoleType FacilityRoleType { get; set; } = null!;
    }
}
