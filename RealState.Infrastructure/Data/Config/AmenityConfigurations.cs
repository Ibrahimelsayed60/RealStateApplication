using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Data.Config
{
    public class AmenityConfigurations : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(A => A.Name).HasMaxLength(100).IsRequired();

            builder.Property(A => A.Description).HasMaxLength(300);

            builder.HasOne(a => a.Villa).WithMany(v => v.Amenities).HasForeignKey(a => a.VillaId);
        }
    }
}
