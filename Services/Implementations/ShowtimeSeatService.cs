using BusinessObjects.Models;
using Common.DTOs.SeatDTOs;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ShowtimeSeatService : IShowtimeSeatService
{
    private readonly IShowtimeSeatRepository _showtimeSeatRepository;

    public ShowtimeSeatService(IShowtimeSeatRepository showtimeSeatRepository)
    {
        _showtimeSeatRepository = showtimeSeatRepository;
    }

    public void InsertShowtimeSeatFromDTO(List<ChosenSeatDTO> dtos)
    {
        if (dtos.IsNullOrEmpty())
        {
            throw new ArgumentException("The list of chosen seats cannot be null or empty.");
        }

        foreach (var dto in dtos)
        {
            if (dto == null)
            {
                throw new ArgumentException("Chosen seat DTO cannot be null.");
            }

            // Check if there is already a seat with the same ShowtimeId and SeatId
            var existingSeat = _showtimeSeatRepository.Get()
                .FirstOrDefault(ss => ss.ShowtimeId == dto.ShowtimeId && ss.SeatId == dto.SeatId);
            if (existingSeat != null)
            {
                throw new Exception($"This showtime seat has already be booked by other user, please try again");
            }
            var seat = new ShowtimeSeat
            {
                Id = Guid.NewGuid(),
                ShowtimeId = dto.ShowtimeId,
                SeatId = dto.SeatId,
                CreatedAt = DateTime.Now,
            };
            Add(seat);
        }
    }

    public IEnumerable<ShowtimeSeat> GetAll()
    {
        return _showtimeSeatRepository.Get().ToList();
    }

    public ShowtimeSeat GetById(Guid id)
    {
        return _showtimeSeatRepository.GetByID(id);
    }

    public void Add(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Insert(showtimeSeat);
        _showtimeSeatRepository.Save();
    }

    public void Update(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Update(showtimeSeat);
        _showtimeSeatRepository.Save();
    }

    public void Delete(Guid id)
    {
        _showtimeSeatRepository.Delete(id);
        _showtimeSeatRepository.Save();
    }

    public void Delete(ShowtimeSeat showtimeSeat)
    {
        _showtimeSeatRepository.Delete(showtimeSeat);
        _showtimeSeatRepository.Save();
    }
}