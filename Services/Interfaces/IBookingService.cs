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
    Guid CreateTemporaryBookingForUser(Guid userId);
    Booking CalculateBookingForUser(Guid bookingId, Guid userId);
    Booking GetReviewBookingVM(Guid bookingId, Guid userId);
} 