using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IMovieDirectorService
{
    IEnumerable<MovieDirector> GetAll();
    MovieDirector GetById(Guid id);
    void Add(MovieDirector movieDirector);
    void Update(MovieDirector movieDirector);
    void Delete(Guid id);
    void Delete(MovieDirector movieDirector);
} 