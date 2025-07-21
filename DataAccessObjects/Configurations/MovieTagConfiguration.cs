using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessObjects.Configurations;

public class MovieTagConfiguration:IEntityTypeConfiguration<MovieTag>
{
    public void Configure(EntityTypeBuilder<MovieTag> builder)
    {
        builder.HasKey(mt => mt.Id);
        
        // MovieTag - Tag
        builder.HasOne(mt => mt.Tag)
            .WithMany(t => t.MovieTags)
            .HasForeignKey(mt => mt.MovieId);
    }
}