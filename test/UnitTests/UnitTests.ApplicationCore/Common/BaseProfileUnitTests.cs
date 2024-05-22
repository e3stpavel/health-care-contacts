using AutoMapper;
using UtterlyComplete.ApplicationCore.Mappings;

namespace UtterlyComplete.UnitTests.ApplicationCore.Common
{
    public class BaseProfileUnitTests
    {
        private readonly MapperConfiguration _mapperConfiguration;

        protected readonly IMapper _mapper;

        public BaseProfileUnitTests()
        {
            // todo: maybe use reflection to load all profiles
            _mapperConfiguration = new(config => config.AddProfiles(
            [
                new PartyProfile(),
                new FacilityProfile(),
                new ContactMechanismProfile()
            ]));

            _mapper = _mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void AutoMapper_Configuration_ShouldBeValid()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
