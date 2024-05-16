using UtterlyComplete.Domain.Common;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;

namespace UnitTests.Infrastructure.Data.TestingUtils
{
    internal static class ShallowEntities
    {
        public static List<Entity> AsList { get; } =
        [
            new Party() { Id = 1 },
            new ElectronicAddress() { Id = 10 },
            new AmbulatorySurgeryCenter() { Id = 3 },
            new Floor() { Id = 5 },
            new TelecommunicationsNumber() { Id = 100 },
        ];

        public static IEnumerable<Entity[]> AsDynamicData => AsList.Select(e => new Entity[] { e });

        public static IQueryable<Entity> AsQueryable => AsList.AsQueryable();
    }
}
