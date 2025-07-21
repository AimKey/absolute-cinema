using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ShowtimeService : IShowtimeService
{
    private readonly IShowtimeRepository _showtimeRepository;

    public ShowtimeService(IShowtimeRepository showtimeRepository)
    {
        _showtimeRepository = showtimeRepository;
    }

    public IEnumerable<Showtime> GetAll()
    {
        return _showtimeRepository.Get().ToList();
    }

    public Showtime GetById(Guid id)
    {
        return _showtimeRepository.GetByID(id);
    }

    public void Add(Showtime showtime)
    {
        _showtimeRepository.Insert(showtime);
        _showtimeRepository.Save();
    }

    public void Update(Showtime showtime)
    {
        _showtimeRepository.Update(showtime);
        _showtimeRepository.Save();
    }

    public void Delete(Guid id)
    {
        _showtimeRepository.Delete(id);
        _showtimeRepository.Save();
    }

    public void Delete(Showtime showtime)
    {
        _showtimeRepository.Delete(showtime);
        _showtimeRepository.Save();
    }
} 