using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class MovieTagService : IMovieTagService
{
    private readonly IMovieTagRepository _movieTagRepository;

    public MovieTagService(IMovieTagRepository movieTagRepository)
    {
        _movieTagRepository = movieTagRepository;
    }

    public IEnumerable<MovieTag> GetAll()
    {
        return _movieTagRepository.Get().ToList();
    }

    public MovieTag GetById(Guid id)
    {
        return _movieTagRepository.GetByID(id);
    }

    public void Add(MovieTag movieTag)
    {
        _movieTagRepository.Insert(movieTag);
        _movieTagRepository.Save();
    }

    public void Update(MovieTag movieTag)
    {
        _movieTagRepository.Update(movieTag);
        _movieTagRepository.Save();
    }

    public void Delete(Guid id)
    {
        _movieTagRepository.Delete(id);
        _movieTagRepository.Save();
    }

    public void Delete(MovieTag movieTag)
    {
        _movieTagRepository.Delete(movieTag);
        _movieTagRepository.Save();
    }
} 