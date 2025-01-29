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
    public class VillaNumberConfigurations : IEntityTypeConfiguration<VillaNumber>
    {
        public void Configure(EntityTypeBuilder<VillaNumber> builder)
        {
            builder.HasOne(v => v.Villa).WithMany().HasForeignKey(v => v.VillaId);

            builder.Property( v => v.Villa_Number).IsRequired();

            builder.Property(v => v.SpecialDetails).HasMaxLength(200);

        }
    }
}
