using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessObjects.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        // Id
        builder.HasKey(m => m.Id);
        // Movie - MovieActors
        builder.HasMany(m => m.MovieActors)
            .WithOne(ma => ma.Movie)
            .HasForeignKey(ma => ma.MovieId);

        // Movie - MovieTags
        builder.HasMany(m => m.MovieTags)
            .WithOne(mt => mt.Movie)
            .HasForeignKey(mt => mt.MovieId);

        // Movie - MovieDirectors
        builder.HasMany(m => m.MovieDirectors)
            .WithOne(md => md.Movie)
            .HasForeignKey(md => md.MovieId);
        
        // Movie? - Reviews (Review has no more config)
        builder.HasMany(m => m.Reviews)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId)
            .IsRequired(false);

    }
}