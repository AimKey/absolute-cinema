using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class MovieDirectorService : IMovieDirectorService
{
    private readonly IMovieDirectorRepository _movieDirectorRepository;

    public MovieDirectorService(IMovieDirectorRepository movieDirectorRepository)
    {
        _movieDirectorRepository = movieDirectorRepository;
    }

    public IEnumerable<MovieDirector> GetAll()
    {
        return _movieDirectorRepository.Get().ToList();
    }

    public MovieDirector GetById(Guid id)
    {
        return _movieDirectorRepository.GetByID(id);
    }

    public void Add(MovieDirector movieDirector)
    {
        _movieDirectorRepository.Insert(movieDirector);
        _movieDirectorRepository.Save();
    }

    public void Update(MovieDirector movieDirector)
    {
        _movieDirectorRepository.Update(movieDirector);
        _movieDirectorRepository.Save();
    }

    public void Delete(Guid id)
    {
        _movieDirectorRepository.Delete(id);
        _movieDirectorRepository.Save();
    }

    public void Delete(MovieDirector movieDirector)
    {
        _movieDirectorRepository.Delete(movieDirector);
        _movieDirectorRepository.Save();
    }
} 