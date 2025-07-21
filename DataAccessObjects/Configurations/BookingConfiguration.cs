using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);
            // Booking - Tickets
            builder.HasMany(b => b.Tickets)
                .WithOne(t => t.Booking)
                .HasForeignKey(t => t.BookingId);
            
            // Booking - payment
            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Booking>(b => b.PaymentId);
        }
    }
}
