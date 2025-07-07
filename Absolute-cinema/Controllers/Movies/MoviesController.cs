
namespace Absolute_cinema.Controllers.Movies;

using Microsoft.AspNetCore.Mvc;
using BusinessObjects.Models;
using Absolute_cinema.Models.ViewModels;
using Services.Interfaces;
using System.Collections.Generic;

public class MoviesController : Controller
{
    // Dependency injection for services
    private readonly IMovieService _movieService;
    private readonly ITagService _tagService;

    // Constructor to inject services
    public MoviesController(IMovieService movieService, ITagService tagService)
    {
        _movieService = movieService;
        _tagService = tagService;
    }


    // Index action to display the list of movies with filters and sorting
    public IActionResult Index(
            string search = "",
            string[]? tags = null,
            string country = "",
            string quality = "",
            string year = "",
            string rating = "",
            string price = "",
            string featured = "",
            string newRelease = "",
            string sort = "newest",
            int page = 1)
    {

        // Get all movies from the service and map to ViewModel
        var movies = MapToListMovieVM(_movieService.GetAll().ToList());
        
        // Apply filters
        if (!string.IsNullOrEmpty(search))
        {
            movies = movies.Where(m =>
                m.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                m.OriginalTitle.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                m.Description.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Apply tag filters
        if (tags != null && tags.Any())
        {
            movies = movies.Where(m => tags.Any(tag => m.MovieTags.Any(mt => tags.Contains(mt.Tag.Name)))).ToList();
        }

        // Apply other filters
        if (!string.IsNullOrEmpty(country))
        {
            movies = movies.Where(m => m.Country == country).ToList();
        }

        if (!string.IsNullOrEmpty(quality))
        {
            movies = movies.Where(m => m.Quality == quality).ToList();
        }

        if (!string.IsNullOrEmpty(year))
        {
            switch (year)
            {
                case "2024":
                case "2023":
                case "2022":
                case "2021":
                case "2020":
                    movies = movies.Where(m => m.ReleaseDate.Year.ToString() == year).ToList();
                    break;
                case "2010s":
                    movies = movies.Where(m => m.ReleaseDate.Year >= 2010 && m.ReleaseDate.Year <= 2019).ToList();
                    break;
                case "2000s":
                    movies = movies.Where(m => m.ReleaseDate.Year >= 2000 && m.ReleaseDate.Year <= 2009).ToList();
                    break;
                case "older":
                    movies = movies.Where(m => m.ReleaseDate.Year < 2000).ToList();
                    break;
            }
        }

        if (!string.IsNullOrEmpty(rating))
        {
            switch (rating)
            {
                case "9+":
                    movies = movies.Where(m => m.ImdbRating >= 9.0m).ToList();
                    break;
                case "8+":
                    movies = movies.Where(m => m.ImdbRating >= 8.0m).ToList();
                    break;
                case "7+":
                    movies = movies.Where(m => m.ImdbRating >= 7.0m).ToList();
                    break;
                case "6+":
                    movies = movies.Where(m => m.ImdbRating >= 6.0m).ToList();
                    break;
            }
        }

        if (!string.IsNullOrEmpty(price))
        {
            switch (price)
            {
                case "free":
                    movies = movies.Where(m => m.IsFree).ToList();
                    break;
                case "paid":
                    movies = movies.Where(m => !m.IsFree).ToList();
                    break;
            }
        }

        if (featured == "true")
        {
            movies = movies.Where(m => m.IsFeatured).ToList();
        }

        if (newRelease == "true")
        {
            movies = movies.Where(m => m.IsNewRelease).ToList();
        }

        // Apply sorting
        switch (sort)
        {
            case "rating":
                movies = movies.OrderByDescending(m => m.ImdbRating).ToList();
                break;
            case "popular":
                movies = movies.OrderByDescending(m => m.ViewCount).ToList();
                break;
            case "title":
                movies = movies.OrderBy(m => m.Title).ToList();
                break;
            case "year":
                movies = movies.OrderByDescending(m => m.ReleaseDate).ToList();
                break;
            case "newest":
            default:
                movies = movies.OrderByDescending(m => m.CreatedAt).ToList();
                break;
        }

        // Pagination
        int pageSize = 12;
        var totalMovies = movies.Count();
        var totalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

        movies = movies.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        // Set ViewBag properties
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.Search = search;
        ViewBag.SelectedTags = tags ?? new string[0];
        ViewBag.Country = country;
        ViewBag.Quality = quality;
        ViewBag.Year = year;
        ViewBag.Rating = rating;
        ViewBag.Price = price;
        ViewBag.Featured = featured;
        ViewBag.NewRelease = newRelease;
        ViewBag.Sort = sort;
        ViewBag.AllTags = GetAllTags();

        // Return the view with the filtered and sorted movies
        return View(movies);
    }


    // Details action to show movie details
    public IActionResult Details(Guid id)
    {
        // Tìm phim theo ID - trong thực tế sẽ query từ database
        var movie = MapToMovieVM(_movieService.GetById(id));

        if (movie == null)
        {
            return NotFound();
        }

        // Lấy phim liên quan (cùng thể loại)
        var relatedMovies = _movieService.GetAll()
            .Where(m => m.Id != id && m.MovieTags.Any(mt => movie.MovieTags.Any(movieTag => movieTag.Tag.Name == mt.Tag.Name)))
            .Take(6)
            .ToList();

        ViewBag.RelatedMovies = MapToListMovieVM(relatedMovies);

        return View(movie);
    }

    // all below is mock data and mapping functions 
    private List<TagVM> GetAllTags()
    {
        List<Tag> tags = _tagService.GetAll().ToList();
        return MaptoTagVM(tags);
    }

    private List<TagVM> MaptoTagVM (List<Tag> tags)
    {
        return tags.Select(t => new TagVM
        {
            Id = t.Id,
            Name = t.Name,
        }).ToList();
    }

    private MovieVM MapToMovieVM(Movie m)
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
            IsNewRelease = true,
            Price = 5000,
            IsFree = true,
            CreatedAt = DateTime.Now,
            MovieActors = MapToMovieActorVM(m.MovieActors?.ToList() ?? new List<MovieActor>()),
            MovieDirectors = MapToMovieDirectorVM(m.MovieDirectors?.ToList() ?? new List<MovieDirector>()),
            MovieTags = MapToMovieTagVM(m.MovieTags?.ToList() ?? new List<MovieTag>())
        };
    } 

    private List<MovieVM> MapToListMovieVM(List<Movie> movies)
    {
        return movies.Select(m => new MovieVM
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
            IsNewRelease =true,
            Price = 5000,
            IsFree = true,
            CreatedAt = DateTime.Now,
            MovieTags = MapToMovieTagVM(m.MovieTags?.ToList() ?? new List<MovieTag>())
        }).ToList();
    }

    private List<MovieTagVM> MapToMovieTagVM(List<MovieTag> movieTags)
    {
        return movieTags.Select(mt => new MovieTagVM
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
        }).ToList();
    }

    private List<MovieActorVM> MapToMovieActorVM(List<MovieActor> movieActors)
    {
        return movieActors.Select(ma => new MovieActorVM
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
        }).ToList();
    }

    private List<MovieDirectorVM> MapToMovieDirectorVM(List<MovieDirector> movieDirectors)
    {
        return movieDirectors.Select(md => new MovieDirectorVM
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
        }).ToList();
    }


}


