using BusinessObjects.Models;

namespace Services.Interfaces;

public interface ISeatTypeService
{
    IEnumerable<SeatType> GetAll();
    SeatType GetById(Guid id);
    void Add(SeatType seatType);
    void Update(SeatType seatType);
    void Delete(Guid id);
    void Delete(SeatType seatType);
} 