using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects.Configurations;

public class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        // id
        builder.HasKey(st => st.Id);
        
        // showtimes ? - movie
        builder.HasOne(st => st.Movie)
            .WithMany(m => m.Showtimes)
            .HasForeignKey(st => st.MovieId)
            .IsRequired(false);
        
        // showtimes ? room
        builder.HasOne(st => st.Room)
            .WithMany(r => r.Showtimes)
            .HasForeignKey(st => st.RoomId)
            .IsRequired(false);
    }
}