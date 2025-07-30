using BusinessObjects.Models;
using Common.ViewModels.BookingVMs;

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
    ReviewBookingVM GetReviewBookingVM(Guid bookingId, Guid userId);
    void UpdateBookingJobCancellationId(Guid bookingId, string jobId);
    void BookingFinished(Guid bookingId, Guid paymentId);
    List<Booking> GetBookingsByUserId(Guid id);
    void CancelBooking(Guid bookingId);
    bool IsUserHasUnpaidBooking(Guid userId);
} 