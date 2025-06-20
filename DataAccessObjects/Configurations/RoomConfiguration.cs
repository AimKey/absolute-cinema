using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessObjects.Configurations;

public class RoomConfiguration: IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
       // Id
       builder.HasKey(r => r.Id);
       // Room - Seats ?
       builder.HasMany(r => r.Seats)
           .WithOne(s => s.Room)
           .HasForeignKey(s => s.RoomId)
           .IsRequired(false);
    }
}