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
            // todo: probably there is a better way to declare entity subtypes

            // register contact mechanisms
            modelBuilder.Entity<PostalAddress>();
            modelBuilder.Entity<TelecommunicationsNumber>();
            modelBuilder.Entity<ElectronicAddress>();

            // register facilities
            modelBuilder.Entity<AmbulatorySurgeryCenter>();
            modelBuilder.Entity<Clinic>();
            modelBuilder.Entity<Floor>();
            modelBuilder.Entity<Hospital>();
            modelBuilder.Entity<MedicalBuilding>();
            modelBuilder.Entity<MedicalOffice>();
            modelBuilder.Entity<Room>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartyEntityTypeConfiguration).Assembly);
        }
    }
}
