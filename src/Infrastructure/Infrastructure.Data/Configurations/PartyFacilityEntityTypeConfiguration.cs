using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtterlyComplete.Domain.Core;

namespace UtterlyComplete.Infrastructure.Data.Configurations
{
    internal class PartyFacilityEntityTypeConfiguration : IEntityTypeConfiguration<PartyFacility>
    {
        public void Configure(EntityTypeBuilder<PartyFacility> builder)
        {
            builder
                .Property(e => e.FacilityRoleTypeId)
                .HasConversion<int>();
        }
    }
}
