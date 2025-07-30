using BusinessObjects.Models;
using Common.DTOs.TicketDTOs;

namespace Services.Interfaces;

public interface ITicketService
{
    IEnumerable<Ticket> GetAll();
    Ticket GetById(Guid id);
    void Add(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Guid id);
    void Delete(Ticket ticket);
    void CreateTicketsForUserBookingFromDto(BookingTicketDTO dto, Guid bookingId, Guid userId);
} 