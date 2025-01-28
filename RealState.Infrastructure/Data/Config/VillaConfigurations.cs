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
    internal class VillaConfigurations : IEntityTypeConfiguration<Villa>
    {
        public void Configure(EntityTypeBuilder<Villa> builder)
        {
            builder.Property(v => v.Name).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Price).HasColumnType("decimal(18,2)");
            builder.Property(v => v.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(v => v.UpdatedDate).HasComputedColumnSql("GETDATE()");

        }
    }
}
