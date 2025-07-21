using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class DirectorService : IDirectorService
{
    private readonly IDirectorRepository _directorRepository;

    public DirectorService(IDirectorRepository directorRepository)
    {
        _directorRepository = directorRepository;
    }

    public IEnumerable<Director> GetAll()
    {
        return _directorRepository.Get().ToList();
    }

    public Director GetById(Guid id)
    {
        return _directorRepository.GetByID(id);
    }

    public void Add(Director director)
    {
        _directorRepository.Insert(director);
        _directorRepository.Save();
    }

    public void Update(Director director)
    {
        _directorRepository.Update(director);
        _directorRepository.Save();
    }

    public void Delete(Guid id)
    {
        _directorRepository.Delete(id);
        _directorRepository.Save();
    }

    public void Delete(Director director)
    {
        _directorRepository.Delete(director);
        _directorRepository.Save();
    }
} 