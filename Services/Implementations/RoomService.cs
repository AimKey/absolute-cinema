using BusinessObjects.Models;
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

} 