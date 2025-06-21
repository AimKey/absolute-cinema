using BusinessObjects.Models;

namespace Services.Interfaces;

public interface IShowtimeService
{
    IEnumerable<Showtime> GetAll();
    Showtime GetById(Guid id);
    void Add(Showtime showtime);
    void Update(Showtime showtime);
    void Delete(Guid id);
    void Delete(Showtime showtime);
} 