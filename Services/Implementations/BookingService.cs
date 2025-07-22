using BusinessObjects.Models;
using Common.DTOs.VNPay;
using Common.ViewModels.BookingVMs;
using Hangfire;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUserService _userService;
    private readonly IShowtimeSeatService _showTimeSeatService;
    private readonly ITicketService _ticketService;
    private readonly IPaymentService paymentService;

    public BookingService(IBookingRepository bookingRepository, IUserService userService, IShowtimeSeatService showTimeSeatService, ITicketService ticketService, IPaymentService paymentService)
    {
        _bookingRepository = bookingRepository;
        _userService = userService;
        _showTimeSeatService = showTimeSeatService;
        _ticketService = ticketService;
        this.paymentService = paymentService;
    }

    public void BookingFinished(Guid bookingId, Guid paymentId)
    {
        var booking = GetById(bookingId);
        if (booking == null)
        {
            throw new ArgumentException("Booking not found", nameof(bookingId));
        }
        var jobId = booking.CancellationJobId;
        if (!string.IsNullOrEmpty(jobId))
        {
            BackgroundJob.Delete(jobId);
        }
        booking.CancellationJobId = null; // Clear the job ID since booking is finished

        // Add a payment to this booking
        var payment = paymentService.GetById(paymentId);
        if (payment == null)
        {
            throw new ArgumentException("Payment not found", nameof(paymentId));
        }
        payment.BookingId = bookingId;

        paymentService.Update(payment);
        Update(booking);
    }

    public void UpdateBookingJobCancellationId(Guid bookingId, string jobId)
    {
        var booking = GetById(bookingId);
        if (booking == null)
        {
            throw new ArgumentException("Booking not found", nameof(bookingId));
        }
        booking.CancellationJobId = jobId;
        Update(booking);
    }

    public void CancelUnpaidBooking(Guid bookingId)
    {
        var booking = GetById(bookingId);
        var showtimeSeats = booking.Tickets.Select(t => t.ShowtimeSeat).ToList();
        foreach (var seat in showtimeSeats)
        {
            _showTimeSeatService.Delete(seat.Id);
        }
        // Just for safe, delete the tickets as well
        booking = GetById(bookingId);
        foreach (var ticket in booking.Tickets)
        {
            _ticketService.Delete(ticket.Id);
        }
        // Then delete self
        Delete(bookingId);
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

    public ReviewBookingVM GetReviewBookingVM(Guid bookingId, Guid userId)
    {
        var booking = GetById(bookingId);
        var user = _userService.GetById(userId);
        var desc = $"{bookingId},{userId},{booking.Tickets.Count()},{booking.Tickets.FirstOrDefault()?.ShowtimeSeat.Showtime.Movie.Title}";
        var reviewBooking = new ReviewBookingVM
        {
            BookingId = booking.Id,
            BookingDate = booking.BookingDate,
            Showtime = booking.Tickets.FirstOrDefault()?.ShowtimeSeat?.Showtime,
            BookedSeat = booking.Tickets.Select(t => t.ShowtimeSeat.Seat).ToList(),
            Tickets = booking.Tickets.ToList(),
            TotalBookingCost = booking.TotalPrice,
            Payment = booking.Payment,
            UserInfo = user,
            PaymentInformationModel = new PaymentInformationModel
            {
                Amount = (double)booking.TotalPrice,
                Name = "",
                OrderDescription = desc,
                OrderType = "Online banking",
            }
        };
        return reviewBooking;
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

    public List<Booking> GetBookingsByUserId(Guid id)
    {
        var bookings = _bookingRepository.Get(b => b.UserId == id).ToList();
        return bookings;
    }
}