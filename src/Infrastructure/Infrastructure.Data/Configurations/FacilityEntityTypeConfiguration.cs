using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Infrastructure.Data.Configurations
{
    internal class FacilityEntityTypeConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder
                .HasDiscriminator(e => e.Type);
        }
    }
}
