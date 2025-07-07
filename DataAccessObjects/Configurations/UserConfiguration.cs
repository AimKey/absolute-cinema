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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(ud => ud.Id);
            // User - Bookings
            builder.HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .IsRequired(false); // Optional relationship

            // User - Reviews
            builder.HasMany(u => u.Reviews)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId) 
                .IsRequired(false);
        }
    }
}
