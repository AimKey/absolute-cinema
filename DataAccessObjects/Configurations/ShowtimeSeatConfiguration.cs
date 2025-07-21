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
    public class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeat>
    {
        public void Configure(EntityTypeBuilder<ShowtimeSeat> builder)
        {
            builder.HasKey(st => st.Id);
            
            // Showtimes? - showtime
            builder.HasOne(sts => sts.Showtime)
                .WithMany(st => st.ShowtimeSeats)
                .HasForeignKey(sts => sts.ShowtimeId)
                .IsRequired(false);
            
            // Showtimes? - seat
            builder.HasOne(sts => sts.Seat)
                .WithMany(seat => seat.ShowtimeSeats)
                .HasForeignKey(sts => sts.SeatId)
                .IsRequired(false);
        }
    }
}