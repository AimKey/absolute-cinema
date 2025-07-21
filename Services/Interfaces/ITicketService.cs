using BusinessObjects.Models;

namespace Services.Interfaces;

public interface ITicketService
{
    IEnumerable<Ticket> GetAll();
    Ticket GetById(Guid id);
    void Add(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Guid id);
    void Delete(Ticket ticket);
} 