using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class SeatTypeService : ISeatTypeService
{
    private readonly ISeatTypeRepository _seatTypeRepository;

    public SeatTypeService(ISeatTypeRepository seatTypeRepository)
    {
        _seatTypeRepository = seatTypeRepository;
    }

    public IEnumerable<SeatType> GetAll()
    {
        return _seatTypeRepository.Get().ToList();
    }

    public SeatType GetById(Guid id)
    {
        return _seatTypeRepository.GetByID(id);
    }

    public void Add(SeatType seatType)
    {
        _seatTypeRepository.Insert(seatType);
        _seatTypeRepository.Save();
    }

    public void Update(SeatType seatType)
    {
        _seatTypeRepository.Update(seatType);
        _seatTypeRepository.Save();
    }

    public void Delete(Guid id)
    {
        _seatTypeRepository.Delete(id);
        _seatTypeRepository.Save();
    }

    public void Delete(SeatType seatType)
    {
        _seatTypeRepository.Delete(seatType);
        _seatTypeRepository.Save();
    }
} 