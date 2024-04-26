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
    internal class FacilityRoleTypeEntityTypeConfiguration : IEntityTypeConfiguration<FacilityRoleType>
    {
        public void Configure(EntityTypeBuilder<FacilityRoleType> builder)
        {
            builder
                .Property(e => e.Id)
                .HasConversion<int>();

            builder
                .HasData(
                    Enum.GetValues(typeof(FacilityRoleTypeId))
                        .Cast<FacilityRoleTypeId>()
                        .Select(e => new FacilityRoleType()
                        {
                            Id = e,
                            Value = e.ToString()
                        })
                );
        }
    }
}
