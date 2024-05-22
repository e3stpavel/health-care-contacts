using FluentAssertions;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.UnitTests.ApplicationCore.Common;

namespace UtterlyComplete.UnitTests.ApplicationCore
{
    [TestClass]
    public class PartyProfileUnitTests : BaseProfileUnitTests
    {
        [TestMethod]
        public void PartyToPartyDto_WhenFacilitiesExist_ShouldHavePartyFacilityDtoList()
        {
            // arrange
            List<PartyFacilityDto> expected = [];

            Party party = new() { Id = 1 };

            FacilityType facilityType = new() { Id = 1, Description = $"test {nameof(FacilityType)}" };
            List<Facility> facilities =
            [
                new Hospital() { Id = 1 },
                new AmbulatorySurgeryCenter() { Id = 2 },
                new Clinic() { Id = 3 }
            ];

            foreach (Facility facility in facilities)
            {
                facility.Description = $"test {facility.GetType().Name}";

                facility.FacilityType = facilityType;
                facility.FacilityTypeId = facilityType.Id;

                facilityType.Facilities.Add(facility);

                FacilityRoleTypeId roleTypeId = facility.Id % 2 == 0 ? FacilityRoleTypeId.LEASED : FacilityRoleTypeId.OWNED;
                FacilityRoleType roleType = new()
                {
                    Id = roleTypeId,
                    Value = Enum.GetName(roleTypeId) ?? ""
                };

                party.Facilities.Add(facility);
                party.PartyFacilities.Add(new()
                {
                    Facility = facility,
                    FacilityId = facility.Id,
                    Party = party,
                    PartyId = party.Id,
                    FacilityRoleTypeId = roleTypeId,
                    FacilityRoleType = roleType
                });

                expected.Add(new(
                    new(facility.Id, default, facility.Description, facilityType.Description, null, null, []),
                    roleType.Value));
            }

            // act
            PartyDto partyDto = _mapper.Map<PartyDto>(party);

            // assert
            List<PartyFacilityDto> actual = [.. partyDto.Facilities];

            actual.Should()
                .BeEquivalentTo(expected);
        }

        [TestMethod]
        public void PartyToPartyDto_WhenFacilitiesIsEmpty_ShouldHaveEmptyList()
        {
            Party party = new() { Id = 1 };

            PartyDto partyDto = _mapper.Map<PartyDto>(party);

            partyDto.Facilities.Should()
                .BeEmpty();
        }

        [TestMethod]
        public void PartyDtoToParty_WhenFacilitiesExist_ShouldHavePartyFacilityList()
        {
            List<FacilityDto> facilityDtos =
            [
                new(1, "hospital", "some", "bruh", default, default, []),
                new(2, "clinic", "thought", "he", default, default, []),
                new(3, "ambulatorySurgeryCenter", "knows", "everything", default, default, [])
            ];

            PartyDto partyDto = new(1, [],
                facilityDtos.Select((facilityDto, i) => new PartyFacilityDto(facilityDto, i % 2 == 0 ? "LEASED" : "BOOKED")).ToList());

            List<PartyFacility> expected = facilityDtos.Select((facilityDto, i) => new PartyFacility()
            {
                FacilityId = facilityDto.Id,
                PartyId = partyDto.Id,
                FacilityRoleTypeId = i % 2 == 0 ? FacilityRoleTypeId.LEASED : FacilityRoleTypeId.BOOKED
            }).ToList();

            Party party = _mapper.Map<Party>(partyDto);

            party.PartyFacilities.Should()
                .BeEquivalentTo(expected);
        }

        [TestMethod]
        public void PartyDtoToParty_WhenFacilitiesIsEmpty_ShouldHaveEmptyList()
        {
            PartyDto partyDto = new(1, [], []);

            Party party = _mapper.Map<Party>(partyDto);

            party.PartyFacilities.Should()
                .BeEmpty();
        }
    }
}