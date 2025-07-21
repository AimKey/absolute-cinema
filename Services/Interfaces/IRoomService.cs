using BusinessObjects.Models;
using Common.ViewModels;

namespace Services.Interfaces;

public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room GetById(Guid id);
    void Add(Room room);
    void Update(Room room);
    void Delete(Guid id);
    void Delete(Room room);
    Room GetRoomFromShowtimeId(Guid showtimeId);
    RoomSeatsVM MapRoomToRoomSeatsVM(Room r, Guid showtimeId);
} 