using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ShowtimeSeatService : IShowtimeSeatService
{
    private readonly IShowtimeSeatRepository _showtimeSeatRepository;

    public ShowtimeSeatService(IShowtimeSeatRepository showtimeSeatRepository)
    {
        _showtimeSeatRepository = showtimeSeatRepository;
    }

    public IEnumerable<ShowtimeSeat> GetAll()
    {
        return _showtimeSeatRepository.Get().ToList();
    }

    public ShowtimeSeat GetById(Guid id)
    {
        return _showtimeSeatRepository.GetByID(id);
    }

    public void Add(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Insert(showtimeSeat);
        _showtimeSeatRepository.Save();
    }

    public void Update(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Update(showtimeSeat);
        _showtimeSeatRepository.Save();
    }

    public void Delete(Guid id)
    {
        _showtimeSeatRepository.Delete(id);
        _showtimeSeatRepository.Save();
    }

    public void Delete(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Delete(showtimeSeat);
        _showtimeSeatRepository.Save();
    }
} 