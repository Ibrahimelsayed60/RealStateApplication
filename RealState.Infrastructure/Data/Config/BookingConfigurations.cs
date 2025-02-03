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
    public class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.BookingEmail).IsRequired();
            
            builder.Property(b => b.BookingName).IsRequired();

            builder.Property(b => b.UserEmail).IsRequired();

            builder.Property(b => b.TotalCost).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(b => b.BookingDate).IsRequired();

            builder.Property(b => b.CheckInDate).IsRequired();

            builder.Property(b =>b.CheckOutDate).IsRequired();  

            builder.HasOne(b => b.Villa).WithMany().HasForeignKey(b => b.VillaId);

            builder.Property(b => b.VillaId).IsRequired();
        }
    }
}
