using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

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
} 