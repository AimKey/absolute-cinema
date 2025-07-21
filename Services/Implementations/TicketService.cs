using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
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
} 