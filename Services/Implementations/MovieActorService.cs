using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class MovieActorService : IMovieActorService
{
    private readonly IMovieActorRepository _movieActorRepository;

    public MovieActorService(IMovieActorRepository movieActorRepository)
    {
        _movieActorRepository = movieActorRepository;
    }

    public IEnumerable<MovieActor> GetAll()
    {
        return _movieActorRepository.Get().ToList();
    }

    public MovieActor GetById(Guid id)
    {
        return _movieActorRepository.GetByID(id);
    }

    public void Add(MovieActor movieActor)
    {
        _movieActorRepository.Insert(movieActor);
        _movieActorRepository.Save();
    }

    public void Update(MovieActor movieActor)
    {
        _movieActorRepository.Update(movieActor);
        _movieActorRepository.Save();
    }

    public void Delete(Guid id)
    {
        _movieActorRepository.Delete(id);
        _movieActorRepository.Save();
    }

    public void Delete(MovieActor movieActor)
    {
        _movieActorRepository.Delete(movieActor);
        _movieActorRepository.Save();
    }
} 