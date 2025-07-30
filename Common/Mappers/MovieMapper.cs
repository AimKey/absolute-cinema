using BusinessObjects.Models;
using Common.DTOs.MovieDTOs;
using Common.ViewModels;

namespace Common.Mappers;

public class MovieMapper
{
    public static Movie MapToMovie(MovieDTO movieDto)
    {
        return new Movie
        {
            Id = movieDto.Id,
            Title = movieDto.Title,
            OriginalTitle = movieDto.OriginalTitle,
            Description = movieDto.Description,
            Duration = movieDto.Duration,
            ReleaseDate = movieDto.ReleaseDate,
            Country = movieDto.Country,
            PosterURL = movieDto.PosterURL,
            BackgroundURL = movieDto.BackgroundURL,
            TrailerURL = movieDto.TrailerURL,
            ImdbRating = movieDto.ImdbRating,
            AgeRestriction = ParseAgeRestrictionFromRating(movieDto.AgeRestriction),
            Status = movieDto.Status,
            CreatedAt = movieDto.CreatedAt ?? DateTime.Now,
            // Các trường navigation như MovieTags, MovieActors... nên được xử lý ở nơi khác
            // Ví dụ như controller hoặc service khi đã xử lý logic liên kết
        };
    }
    public static Movie MapToMovie(UpdateMovieDTO movieDto)
    {
        return new Movie
        {
            Id = movieDto.Id,
            Title = movieDto.Title,
            OriginalTitle = movieDto.OriginalTitle,
            Description = movieDto.Description,
            Duration = movieDto.Duration,
            ReleaseDate = movieDto.ReleaseDate,
            Country = movieDto.Country,
            PosterURL = movieDto.PosterURL,
            BackgroundURL = movieDto.BackgroundURL,
            TrailerURL = movieDto.TrailerURL,
            ImdbRating = movieDto.ImdbRating,
            AgeRestriction = ParseAgeRestrictionFromRating(movieDto.AgeRestriction),
            Status = movieDto.Status,
            CreatedAt = movieDto.CreatedAt ?? DateTime.Now,
            // Các trường navigation như MovieTags, MovieActors... nên được xử lý ở nơi khác
            // Ví dụ như controller hoặc service khi đã xử lý logic liên kết
        };
    }


    private static int ParseAgeRestrictionFromRating(string rating)
    {
        return rating switch
        {
            "G" => 0,          // Mọi lứa tuổi
            "PG" => 10,        // Có thể cần hướng dẫn
            "PG-13" => 13,     // Trên 13 tuổi
            "R" => 17,         // Trên 17 tuổi
            "NC-17" => 18,     // Chỉ người lớn
            _ => 0             // Mặc định
        };
    }

    private static string GetRatingFromAge(int age)
    {
        return age switch
        {
            <= 0 => "G",         
            <= 10 => "PG",       
            <= 13 => "PG-13", 
            <= 17 => "R",    
            _ => "NC-17"         
        };
    }


    public static MovieVM MapToMovieVM(Movie m)
    {
        return new MovieVM
        {
            Id = m.Id,
            Title = m.Title,
            OriginalTitle = m.OriginalTitle,
            Description = m.Description,
            Duration = m.Duration,
            ReleaseDate = m.ReleaseDate,
            Country = m.Country,
            PosterURL = m.PosterURL,
            BackgroundURL = m.BackgroundURL,
            TrailerURL = m.TrailerURL,
            ImdbRating = m.ImdbRating,
            AgeRestriction = m.AgeRestriction,
            Status = m.Status,
            Language = "ENG",
            Quality = "HD",
            ViewCount = 1000,
            IsFeatured = true,
            IsNewRelease = m.ReleaseDate >= DateTime.Now.AddDays(-7) && m.ReleaseDate <= DateTime.Now,
            Price = 10000,
            IsFree = false,
            CreatedAt = DateTime.Now,

            MovieTags = m.MovieTags?.Select(mt => new MovieTagVM
            {
                Id = mt.Id,
                MovieId = mt.MovieId,
                TagId = mt.TagId,
                Tag = new TagVM
                {
                    Id = mt.Tag.Id,
                    Name = mt.Tag.Name,
                    Color = "#ef4444"
                }
            }).ToList() ?? new List<MovieTagVM>(),

            MovieActors = m.MovieActors?.Select(ma => new MovieActorVM
            {
                Id = ma.Id,
                MovieId = ma.MovieId,
                ActorId = ma.ActorId,
                Actor = new ActorVM
                {
                    Id = ma.Actor.Id,
                    Name = ma.Actor.Name,
                    AvatarURL = ma.Actor.AvatarURL
                }
            }).ToList() ?? new List<MovieActorVM>(),

            MovieDirectors = m.MovieDirectors?.Select(md => new MovieDirectorVM
            {
                Id = md.Id,
                MovieId = md.MovieId,
                DirectorId = md.DirectorId,
                Director = new DirectorVM
                {
                    Id = md.Director.Id,
                    Name = md.Director.Name,
                    AvatarURL = md.Director.AvatarURL
                }
            }).ToList() ?? new List<MovieDirectorVM>(),

            Reviews = m.Reviews?.Select(md => new ReviewVM
            {
                Id = md.Id,
                Rating = md.Rating,
                Content = md.Content,
                CreatedAt = md.CreatedAt,
                ReviewDate = md.ReviewDate,
                User = md.User
            }).ToList() ?? new List<ReviewVM>()

        };
    }


    public static MovieDTO MapToMovieDTO(Movie m)
    {
        return new MovieDTO
        {
            Id = m.Id,
            Title = m.Title,
            OriginalTitle = m.OriginalTitle,
            Description = m.Description,
            Duration = m.Duration,
            ReleaseDate = m.ReleaseDate,
            Country = m.Country,
            PosterURL = m.PosterURL,
            BackgroundURL = m.BackgroundURL,
            TrailerURL = m.TrailerURL,
            ImdbRating = m.ImdbRating,
            AgeRestriction = GetRatingFromAge(m.AgeRestriction),
            Status = m.Status,
            Price = 5000,
            CreatedAt = DateTime.Now
        };
    }

    public static UpdateMovieDTO MapToUpdateMovieDTO(Movie m)
    {
        return new UpdateMovieDTO
        {
            Id = m.Id,
            Title = m.Title,
            OriginalTitle = m.OriginalTitle,
            Description = m.Description,
            Duration = m.Duration,
            ReleaseDate = m.ReleaseDate,
            Country = m.Country,
            PosterURL = m.PosterURL,
            BackgroundURL = m.BackgroundURL,
            TrailerURL = m.TrailerURL,
            ImdbRating = m.ImdbRating,
            AgeRestriction = GetRatingFromAge(m.AgeRestriction),
            Status = m.Status,
            Price = 5000,
            CreatedAt = DateTime.Now,
        };
    }

}
