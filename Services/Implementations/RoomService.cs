using BusinessObjects.Models;
using Common.DTOs.RoomDTOs;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public IEnumerable<Room> GetAll()
    {
        return _roomRepository.Get().ToList();
    }

    public Room GetById(Guid id)
    {
        return _roomRepository.GetByID(id);
    }

    public void Add(Room room)
    {
        _roomRepository.Insert(room);
        _roomRepository.Save();
    }

    public void Update(Room room)
    {
        _roomRepository.Update(room);
        _roomRepository.Save();
    }

    public void Delete(Guid id)
    {
        _roomRepository.Delete(id);
        _roomRepository.Save();
    }

    public void Delete(Room room)
    {
        _roomRepository.Delete(room);
        _roomRepository.Save();
    }

    public void AddNewRoom(CreateRoomDTO createRoomDTO)
    {
        // new room 
        Room newRoom = new Room
        {
            Name = createRoomDTO.Name,
            Capacity = createRoomDTO.Capacity,
            ScreenType = createRoomDTO.ScreenType,
            Description = createRoomDTO.Description,
            CreatedAt = DateTime.Now,
        };

        // save db
        Add(newRoom);
    }

    public void UpdateRoom(UpdateRoomDTO room)
    {
        // Check if the room exists
        Room existRoom = GetById(room.Id);
        if (existRoom == null) { 
            throw new KeyNotFoundException();
        }

        // update properties
        existRoom.Name = room.Name;
        existRoom.Capacity = room.Capacity;
        existRoom.ScreenType = room.ScreenType;
        existRoom.Description = room.Description;
        existRoom.UpdatedAt = DateTime.Now;

        // save db
        Update(existRoom);
    }
} 