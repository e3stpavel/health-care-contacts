using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.ContactMechanisms;
using UtterlyComplete.Domain.Core;
using UtterlyComplete.Domain.Facilities;

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

            modelBuilder.Entity<Party>()
                .HasMany(e => e.ContactMechanisms)
                .WithMany(e => e.Parties)
                .UsingEntity<PartyContactMechanism>(
                    j => j.HasKey(e => new { e.PartyId, e.ContactMechanismId, e.FromDate }));
        }
    }
}
