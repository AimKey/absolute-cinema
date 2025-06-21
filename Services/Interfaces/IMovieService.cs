using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IMovieService
{
    IEnumerable<Movie> GetAll();
    Movie GetById(Guid id);
    void Add(Movie movie);
    void Update(Movie movie);
    void Delete(Guid id);
    void Delete(Movie movie);
} 