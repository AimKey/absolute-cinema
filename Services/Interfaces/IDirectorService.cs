using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IDirectorService
{
    IEnumerable<Director> GetAll();
    Director GetById(Guid id);
    void Add(Director director);
    void Update(Director director);
    void Delete(Guid id);
    void Delete(Director director);
} 