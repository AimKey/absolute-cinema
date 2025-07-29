using BusinessObjects.Models;
using Common.DTOs.SeatDTOs;
using Common.DTOs.TicketDTOs;
using Common.ViewModels.ShowtimeVMs;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IShowtimeSeatService _showtimeSeatService;
    private readonly IShowtimeService _showtimeService;
    private readonly ISeatService _seatService;

    public TicketService(
        ITicketRepository ticketRepository,
        IShowtimeSeatService showtimeSeatService,
        IShowtimeService showtimeService,
        ISeatService seatService)
    {
        _ticketRepository = ticketRepository;
        _showtimeSeatService = showtimeSeatService;
        _showtimeService = showtimeService;
        _seatService = seatService;
    }


    public IEnumerable<Ticket> GetAll()
    {
        return _ticketRepository.Get().ToList();
    }

    public Ticket GetById(Guid id)
    {
        return _ticketRepository.GetByID(id);
    }

    public void Add(Ticket ticket)
    {
        _ticketRepository.Insert(ticket);
        _ticketRepository.Save();
    }

    public void Update(Ticket ticket)
    {
        _ticketRepository.Update(ticket);
        _ticketRepository.Save();
    }

    public void Delete(Guid id)
    {
        _ticketRepository.Delete(id);
        _ticketRepository.Save();
    }

    public void Delete(Ticket ticket)
    {
        _ticketRepository.Delete(ticket);
        _ticketRepository.Save();
    }

    public void CreateTicketsForUserBookingFromDto(BookingTicketDTO dto, Guid bookingId, Guid userId)
    {
        var showtimeSeats = _showtimeSeatService.GetAll();
        foreach (var seat in dto.ChosenSeats)
        {
            // Find the showtime seat corresponding to this ticket
            var showtimeSeat = showtimeSeats.FirstOrDefault(ss => ss.SeatId == seat.SeatId && ss.ShowtimeId == seat.ShowtimeId);
            if (showtimeSeat == null)
            {
                throw new ArgumentException($"Server error: No showtime seat to create ticket for");
            }
            var ticketPrice = GetFinalPriceForSeat(showtimeSeat.SeatId, showtimeSeat.ShowtimeId);
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                TicketCode = Guid.NewGuid().ToString(),
                Price = ticketPrice,
                BookingId = bookingId,
                ShowtimeSeatId = showtimeSeat.Id,
                CreatedAt = DateTime.Now,
                CreatedBy = userId
            };
            Add(ticket);
        }
    }

    private decimal GetFinalPriceForSeat(Guid seatId, Guid showtimeId)
    {
        var originalSeat = _seatService.GetById(seatId);
        if (originalSeat == null)
        {
            throw new ArgumentException($"Seat with ID {seatId} not found.");
        }
        decimal seatMultiplier = originalSeat.SeatType.PriceMutiplier;

        var showtime = _showtimeService.GetById(showtimeId);
        if (showtime == null)
        {
            throw new ArgumentException($"Showtime with ID {showtimeId} not found.");
        }
        decimal basePrice = showtime.BasePrice;

        return basePrice * seatMultiplier;
    }
}