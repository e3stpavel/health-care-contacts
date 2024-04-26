using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Infrastructure.Data.Configurations
{
    internal class PartyEntityTypeConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder
                .HasMany(e => e.ContactMechanisms)
                .WithMany(e => e.Parties)
                .UsingEntity<PartyContactMechanism>(
                    j => j.HasKey(e => new { e.PartyId, e.ContactMechanismId, e.FromDate }));
        }
    }
}
