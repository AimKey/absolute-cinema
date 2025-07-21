using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class SeatService : ISeatService
{
    private readonly ISeatRepository _seatRepository;

    public SeatService(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    public IEnumerable<Seat> GetAll()
    {
        return _seatRepository.Get().ToList();
    }

    public Seat GetById(Guid id)
    {
        return _seatRepository.GetByID(id);
    }

    public void Add(Seat seat)
    {
        _seatRepository.Insert(seat);
        _seatRepository.Save();
    }

    public void Update(Seat seat)
    {
        _seatRepository.Update(seat);
        _seatRepository.Save();
    }

    public void Delete(Guid id)
    {
        _seatRepository.Delete(id);
        _seatRepository.Save();
    }

    public void Delete(Seat seat)
    {
        _seatRepository.Delete(seat);
        _seatRepository.Save();
    }
} 