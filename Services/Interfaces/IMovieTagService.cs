using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IMovieTagService
{
    IEnumerable<MovieTag> GetAll();
    MovieTag GetById(Guid id);
    void Add(MovieTag movieTag);
    void Update(MovieTag movieTag);
    void Delete(Guid id);
    void Delete(MovieTag movieTag);
} 