using System.Diagnostics.CodeAnalysis;

namespace UtterlyComplete.ApplicationCore.Models
{
    public record PartyFacilityDto : FacilityDto
    {
        public required string RoleType { get; init; }

        [SetsRequiredMembers]
        public PartyFacilityDto(FacilityDto facility, string roleType) : base(facility)
        {
            RoleType = roleType;
        }
    }
}
