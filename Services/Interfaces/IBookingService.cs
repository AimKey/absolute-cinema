using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IBookingService
{
    IEnumerable<Booking> GetAll();
    Booking GetById(Guid id);
    void Add(Booking booking);
    void Update(Booking booking);
    void Delete(Guid id);
    void Delete(Booking booking);
} 