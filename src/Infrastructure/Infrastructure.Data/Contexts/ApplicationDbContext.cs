using Microsoft.EntityFrameworkCore;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;
using UtterlyComplete.Infrastructure.Data.Configurations;

namespace UtterlyComplete.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Party> Parties { get; set; } = null!;

        public DbSet<Facility> Facilities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisterEntityDerivedTypes<ContactMechanism, PostalAddress>(modelBuilder);
            RegisterEntityDerivedTypes<Facility, AmbulatorySurgeryCenter>(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartyEntityTypeConfiguration).Assembly);
        }

        private static void RegisterEntityDerivedTypes<TBase, TDerived>(ModelBuilder modelBuilder)
        {
            Type someDerivedType = typeof(TDerived);

            foreach (Type type in someDerivedType.Assembly.GetTypes()
                .Where(t => t.Namespace == someDerivedType.Namespace && t.IsClass && t.IsSubclassOf(typeof(TBase))))
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
