using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IMovieActorService
{
    IEnumerable<MovieActor> GetAll();
    MovieActor GetById(Guid id);
    void Add(MovieActor movieActor);
    void Update(MovieActor movieActor);
    void Delete(Guid id);
    void Delete(MovieActor movieActor);
} 