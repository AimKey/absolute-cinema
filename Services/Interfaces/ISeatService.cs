using BusinessObjects.Models;

namespace Services.Interfaces;

public interface ISeatService
{
    IEnumerable<Seat> GetAll();
    Seat GetById(Guid id);
    void Add(Seat seat);
    void Update(Seat seat);
    void Delete(Guid id);
    void Delete(Seat seat);
} 