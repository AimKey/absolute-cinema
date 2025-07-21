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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            
            // Ticket - Showtime seat
            builder.HasOne(t => t.ShowtimeSeat)
                .WithOne(st => st.Ticket)
                .HasForeignKey<ShowtimeSeat>(st => st.TicketId);
        }
    }
}