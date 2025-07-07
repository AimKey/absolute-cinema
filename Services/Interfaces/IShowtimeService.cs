using BusinessObjects.Models;
using Common.ViewModels.ShowtimeVMs;

namespace Services.Interfaces;

public interface IShowtimeService
{
    ViewAllShowtimeVM GetAllVM(ViewAllShowtimeVM vm = null);
    IEnumerable<Showtime> GetAll();
    Showtime GetById(Guid id);
    void Add(Showtime showtime);
    void Update(Showtime showtime);
    void Delete(Guid id);
    void Delete(Showtime showtime);
}