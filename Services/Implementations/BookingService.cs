using BusinessObjects.Models;
using Common.ViewModels.BookingVMs;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserService _userService;

    public BookingService(IBookingRepository bookingRepository, IUserService userService)
    {
        _bookingRepository = bookingRepository;
        _userService = userService;
    }


    public Guid CreateTemporaryBookingForUser(Guid userId)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userId
        };
        Add(booking);
        return booking.Id;
    }

    public Booking CalculateBookingForUser(Guid bookingId, Guid userId)
    {
        var booking = GetById(bookingId);
        var user = _userService.GetById(userId);

        booking.NumberOfTickets = booking.Tickets.Count();
        booking.TotalPrice = booking.Tickets.Sum(t => t.Price);

        Update(booking);
        return booking;
    }

    public Booking GetReviewBookingVM(Guid bookingId, Guid userId)
    {
        var booking = GetById(bookingId);
        var user = _userService.GetById(userId);
        var reviewBooking = new ReviewBookingVM
        {
            BookingId = booking.Id,
            Showtime = booking.Tickets.FirstOrDefault()?.ShowtimeSeat?.Showtime,
            BookedSeat = booking.Tickets.Select(t => t.ShowtimeSeat.Seat).ToList(),
            Tickets = booking.Tickets.ToList(),
            TotalBookingCost = booking.TotalPrice,
            Payment = booking.Payment,
            UserInfo = user
        };
        return booking;
    }

    public IEnumerable<Booking> GetAll()
    {
        return _bookingRepository.Get().ToList();
    }

    public Booking GetById(Guid id)
    {
        return _bookingRepository.GetByID(id);
    }

    public void Add(Booking booking)
    {
        _bookingRepository.Insert(booking);
        _bookingRepository.Save();
    }

    public void Update(Booking booking)
    {
        _bookingRepository.Update(booking);
        _bookingRepository.Save();
    }

    public void Delete(Guid id)
    {
        _bookingRepository.Delete(id);
        _bookingRepository.Save();
    }

    public void Delete(Booking booking)
    {
        _bookingRepository.Delete(booking);
        _bookingRepository.Save();
    }
} 