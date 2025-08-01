using BusinessObjects.Models;
using Common.DTOs.SeatDTOs;

namespace Services.Interfaces;

public interface IShowtimeSeatService
{
    IEnumerable<ShowtimeSeat> GetAll();
    ShowtimeSeat GetById(Guid id);
    void Add(ShowtimeSeat showtimeSeat);
    void Update(ShowtimeSeat showtimeSeat);
    void Delete(Guid id);
    void Delete(ShowtimeSeat showtimeSeat);
    void InsertShowtimeSeatFromDTO(List<ChosenSeatDTO> dtos);
} 