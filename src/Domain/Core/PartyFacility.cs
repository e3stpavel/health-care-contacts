namespace UtterlyComplete.Domain.Core
{
    public class PartyFacility
    {
        public int PartyId { get; set; }

        public int FacilityId { get; set; }

        public FacilityRoleTypeId FacilityRoleTypeId { get; set; }

        public virtual FacilityRoleType FacilityRoleType { get; set; } = null!;
    }
}
