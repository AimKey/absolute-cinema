using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Common.Pagination;
using Common.Filters.Movies;
using Common.Constants;
using Common.ViewModels;
using Common.Mappers;
using Common.DTOs.MovieDTOs;

namespace Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IDirectorRepository _directorRepository;


    public MovieService(
        IMovieRepository movieRepository, 
        ITagRepository tagRepository, 
        IActorRepository actorRepository, 
        IDirectorRepository directorRepository)
    {
        _movieRepository = movieRepository;
        _tagRepository = tagRepository;
        _actorRepository = actorRepository;
        _directorRepository = directorRepository;
    }

    // common CRUD operations for movies
    public IEnumerable<Movie> GetAll()
    {
        return _movieRepository.Get().ToList();
    }

    public Movie GetById(Guid id)
    {
        return _movieRepository.GetByID(id);
    }

    public void Add(Movie movie)
    {
        _movieRepository.Insert(movie);
        _movieRepository.Save();
    }

    public void Update(Movie movie)
    {
        _movieRepository.Update(movie);
        _movieRepository.Save();
    }

    public void Delete(Guid id)
    {
        _movieRepository.Delete(id);
        _movieRepository.Save();
    }

    public void Delete(Movie movie)
    {
        _movieRepository.Delete(movie);
        _movieRepository.Save();
    }

    // enhanced methods
    public PagedResult<MovieVM> FilterMovies(MovieFilterCriteria criteria, bool isAdmin)
    {
        // Get movies        
        var movies = _movieRepository.Get()
                    .AsQueryable();

        // if admin , include hidden movies
        if (!isAdmin)
        {
            movies = movies.Where(m => !m.Status.Equals(MovieStatusConstant.HIDDEN));
        }

        /** Filter logic */
        // Filter by search term
        if (!string.IsNullOrEmpty(criteria.Search))
        {
            movies = movies.Where(m =>
                m.Title.Contains(criteria.Search, StringComparison.OrdinalIgnoreCase) ||
                m.OriginalTitle.Contains(criteria.Search, StringComparison.OrdinalIgnoreCase));
        }

        // Filter by tags
        if (criteria.Tags.Any())
        {
            movies = movies.Where(m => m.MovieTags.Any(mt => criteria.Tags.Contains(mt.Tag.Name)));
        }

        // Filter by country
        if (!string.IsNullOrEmpty(criteria.Country))
            movies = movies.Where(m => m.Country == criteria.Country);

        // Filter by release date
        if (!string.IsNullOrEmpty(criteria.Year))
        {
            movies = criteria.Year switch
            {
                "2010s" => movies.Where(m => m.ReleaseDate.Year >= 2010 && m.ReleaseDate.Year <= 2019),
                "2000s" => movies.Where(m => m.ReleaseDate.Year >= 2000 && m.ReleaseDate.Year <= 2009),
                "older" => movies.Where(m => m.ReleaseDate.Year < 2000),
                _ => movies.Where(m => m.ReleaseDate.Year.ToString() == criteria.Year)
            };
        }

        // Filter by rating
        if (!string.IsNullOrEmpty(criteria.Rating))
        {
            decimal minRating = criteria.Rating switch
            {
                "9+" => 9.0m,
                "8+" => 8.0m,
                "7+" => 7.0m,
                "6+" => 6.0m,
                _ => 0
            };
            movies = movies.Where(m => m.ImdbRating >= minRating);
        }

        // new 
        if (criteria.NewRelease == true)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            movies = movies.Where(m => m.ReleaseDate >= sevenDaysAgo && m.ReleaseDate <= DateTime.Now);
        }
            

        movies = criteria.Sort switch
        {
            "rating" => movies.OrderByDescending(m => m.ImdbRating),
            "cinema" => movies.Where(m => m.Showtimes.Any(st => st.StartTime >= DateTime.Now))
                                .OrderByDescending(m => m.ReleaseDate),
            "title" => movies.OrderBy(m => m.Title),
            "year" => movies.OrderByDescending(m => m.ReleaseDate),
            _ => movies.OrderByDescending(m => m.CreatedAt)
        };

        // Pagination
        int total = movies.Count();
        int pageSize = PageConstants.PageSize;
        var pageItems = movies.Skip((criteria.Page - 1) * pageSize).Take(pageSize).ToList();

        var movieVMs = pageItems.Select(m => MovieMapper.MapToMovieVM(m)).ToList();

        return new PagedResult<MovieVM>
        {
            Items = movieVMs,
            TotalCount = total,
            PageSize = pageSize,
            CurrentPage = criteria.Page
        };
    }

    public MovieVM GetMovieVMById(Guid id)
    {
        var movie = _movieRepository.GetByID(id);
        
        // if null
        if (movie == null)
        {
            throw new KeyNotFoundException();
        }

        // if movie hidden
        if (movie.Status.Equals(MovieStatusConstant.HIDDEN) || movie.RemovedAt.HasValue)
        {
            throw new KeyNotFoundException();
        }

        return MovieMapper.MapToMovieVM(movie);
    }

    public List<MovieVM> GetRalatedMovies(MovieVM movie)
    {
        return _movieRepository.Get()
            .Where(m => !m.Status.Equals(MovieStatusConstant.HIDDEN))
            .Where(m => m.Id != movie.Id && m.MovieTags.Any(mt => movie.MovieTags.Any(movieTag => movieTag.Tag.Name == mt.Tag.Name)))
            .Take(6).Select(m => MovieMapper.MapToMovieVM(m))
            .ToList();
    }

    public void AddNewMovie(MovieDTO movieDto)
    {
        var newMovie = MovieMapper.MapToMovie(movieDto);

        // Set tags
        newMovie.MovieTags = _tagRepository.Get()
            .Where(t => movieDto.SelectedTags.Contains(t.Name))
            .Select(t => new MovieTag
            {
                MovieId = newMovie.Id,
                TagId = t.Id,
            })
            .ToList();

        // TODO: Set actors, directors
        newMovie.MovieActors = _actorRepository.Get()
            .Where(a => movieDto.SelectedActorIds.Contains(a.Id))
            .Select(a => new MovieActor
            {
                MovieId = newMovie.Id,
                ActorId = a.Id,
            })
            .ToList();

        newMovie.MovieDirectors = _directorRepository.Get()
            .Where(d => movieDto.SelectedDirectorIds.Contains(d.Id))
            .Select(d => new MovieDirector
            {
                MovieId = newMovie.Id,
                DirectorId = d.Id,
            })
            .ToList();

        // Add movie
        Add(newMovie);
    }

    public void UpdateMovie(UpdateMovieDTO updateMovieDTO)
    {
        var existingMovie = _movieRepository.GetByID(updateMovieDTO.Id);

        if (existingMovie == null)
        {
            throw new KeyNotFoundException($"Movie with ID {updateMovieDTO.Id} not found.");
        }

        existingMovie.Title = updateMovieDTO.Title;
        existingMovie.OriginalTitle = updateMovieDTO.OriginalTitle;
        existingMovie.Description = updateMovieDTO.Description;
        existingMovie.Duration = updateMovieDTO.Duration;
        existingMovie.ReleaseDate = updateMovieDTO.ReleaseDate;
        existingMovie.Country = updateMovieDTO.Country;
        existingMovie.PosterURL = updateMovieDTO.PosterURL;
        existingMovie.BackgroundURL = updateMovieDTO.BackgroundURL;
        existingMovie.TrailerURL = updateMovieDTO.TrailerURL;
        existingMovie.ImdbRating = updateMovieDTO.ImdbRating;
        existingMovie.AgeRestriction = updateMovieDTO.AgeRestriction switch
        {
            "G" => 0,         
            "PG" => 10,      
            "PG-13" => 13,   
            "R" => 17,         
            "NC-17" => 18,    
            _ => 0           
        };
        existingMovie.Status = updateMovieDTO.Status;
        existingMovie.CreatedAt = updateMovieDTO.CreatedAt ?? DateTime.Now;
        existingMovie.UpdatedAt = DateTime.Now; 
        existingMovie.MovieTags.ToList().Clear(); 
        existingMovie.MovieActors.ToList().Clear();
        existingMovie.MovieDirectors.ToList().Clear();

        // Set tags
        existingMovie.MovieTags = _tagRepository.Get()
            .Where(t => updateMovieDTO.SelectedTags.Contains(t.Name))
            .Select(t => new MovieTag
            {
                MovieId = existingMovie.Id,
                TagId = t.Id,
            })
            .ToList();

        // TODO: Set actors, directors
        existingMovie.MovieActors = _actorRepository.Get()
            .Where(a => updateMovieDTO.SelectedActorIds.Contains(a.Id))
            .Select(a => new MovieActor
            {
                MovieId = existingMovie.Id,
                ActorId = a.Id,
            })
            .ToList();

        existingMovie.MovieDirectors = _directorRepository.Get()
            .Where(d => updateMovieDTO.SelectedDirectorIds.Contains(d.Id))
            .Select(d => new MovieDirector
            {
                MovieId = existingMovie.Id,
                DirectorId = d.Id,
            })
            .ToList();

        // Add movie
        Update(existingMovie);
    }
}