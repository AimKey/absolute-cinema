using BusinessObjects.Models;
using Common.DTOs.ShowtimeDTOs;
using Common.ViewModels.ShowtimeVMs;

namespace Services.Interfaces;

public interface IShowtimeService
{
    ViewAllShowtimeVM GetAllVM(ViewAllShowtimeVM vm = null);
    IEnumerable<Showtime> GetAll();
    Showtime GetById(Guid id);
    void Add(Showtime showtime);
    void AddShowtimeDTO(CreateShowtimeDTO createShowtimeDTO);
    void Update(Showtime showtime);
    void Delete(Guid id);
    void Delete(Showtime showtime);
    List<PreviewRoomShowtimeVM> GetCurrentShowtimeOfRoomInADay(Guid roomId, DateTime date);
}