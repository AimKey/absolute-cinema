using BusinessObjects.Models;
using Common.DTOs.RoomDTOs;

namespace Services.Interfaces;

public interface IRoomService
{
    IEnumerable<Room> GetAll();
    Room GetById(Guid id);
    void Add(Room room);
    void Update(Room room);
    void Delete(Guid id);
    void Delete(Room room);
    void AddNewRoom(CreateRoomDTO createRoomDTO);
    void UpdateRoom(UpdateRoomDTO room);
} 