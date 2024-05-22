using FluentAssertions;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.UnitTests.ApplicationCore.Common;

namespace UtterlyComplete.UnitTests.ApplicationCore
{
    [TestClass]
    public class FacilityProfileUnitTests : BaseProfileUnitTests
    {
        [TestMethod]
        public void FacilityToFacilityDto_ShouldBeCamelCaseType()
        {
            // arrange
            FacilityType facilityType = new() { Id = 1, Description = "test type" };
            
            List<Facility> facilities =
            [
                new AmbulatorySurgeryCenter() { Id = 1 },
                new Hospital() { Id = 2 },
                new Clinic() { Id = 3 },
                new Floor() { Id = 4 }
            ];

            facilities.ForEach(facility =>
            {
                facility.Description = $"test {facility.GetType().Name}";

                facility.FacilityType = facilityType;
                facility.FacilityTypeId = facilityType.Id;

                facilityType.Facilities.Add(facility);
            });

            // act
            List<FacilityDto> facilityDtos = _mapper.Map<List<FacilityDto>>(facilities);

            // assert
            for (int i = 0; i < facilityDtos.Count; i++)
            {
                string facilityTypeName = facilities.ElementAt(i).GetType().Name;
                FacilityDto facilityDto = facilityDtos.ElementAt(i);

                // 'Type' property is private by design as ef core is responsible to set it
                Assert.IsNull(facilityDto.Type);

                //Assert.AreEqual(
                //    facilityTypeName[..1].ToLower() + facilityTypeName[1..], facilityDto.Type);
            }
        }

        public static IEnumerable<object[]> ShallowFacilityDtos
        {
            get
            {
                return
                [
                    [new FacilityDto(1, "ambulatorySurgeryCenter", "some", "stuff", default, default, []), typeof(AmbulatorySurgeryCenter)],
                    [new FacilityDto(2, "clinic", "all", "right", default, default, []), typeof(Clinic)],
                    [new FacilityDto(3, "hospital", "lets", "go", default, default, []), typeof(Hospital)],
                    [new FacilityDto(4, "floor", "hey", "there", default, default, []), typeof(Floor)],
                ];
            }
        }

        [TestMethod]
        [DynamicData(nameof(ShallowFacilityDtos))]
        public void FacilityDtoToFacility_WhenTypeExists_ShouldBeOfType(FacilityDto facilityDto, Type expectedType)
        {
            Facility facility = _mapper.Map<Facility>(facilityDto);

            facility.Should()
                .BeOfType(expectedType);
        }

        [TestMethod]
        public void FacilityDtoToFacility_WhenTypeNotExists_ShouldThrowException()
        {
            string type = "modernWorld";
            FacilityDto facilityDto = new(1, type, "some", "thing", default, default, []);
            Action mapping = () => _mapper.Map<Facility>(facilityDto);

            mapping.Should()
                .ThrowExactly<NotImplementedException>()
                .WithMessage($"*'{type}'*");
        }
    }
}
