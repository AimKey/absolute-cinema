using BusinessObjects.Models;
using Common.Mappers;
using Common.ViewModels;
using Common.ViewModels.SeatTypeVMs;
using Common.ViewModels.SeatVMs;
using Common.DTOs.RoomDTOs;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IShowtimeService _showtimeService;
    private readonly ISeatTypeService _seatTypeService;

    public RoomService(IRoomRepository roomRepository, IShowtimeService showtimeService, ISeatTypeService seatTypeService)
    {
        _roomRepository = roomRepository;
        _showtimeService = showtimeService;
        _seatTypeService = seatTypeService;
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

    public Room GetRoomFromShowtimeId(Guid showtimeId)
    {
        var showtime = _showtimeService.GetById(showtimeId);
        if (showtime != null)
        {
            var room = GetById(showtime.RoomId);
            if (room != null)
            {
                return room;
            }
            else
            {
                throw new Exception("Room not found for the given showtime ID.");
            }
        }
        else
        {
            throw new Exception("Showtime not found for the given ID.");
        }
    }

    public RoomSeatsVM MapRoomToRoomSeatsVM(Room r, Guid showtimeId)
    {
        var showtime = _showtimeService.GetById(showtimeId);
        var seatTypes = _seatTypeService.GetAll();
        List<SeatWithStatusVM> seatsWithStatus = new();

        // Start mapping each seat to its status
        foreach (var seat in r.Seats)
        {
            var showtimeSeats = seat.ShowtimeSeats.ToList();
               
            var vm = RoomSeatMapper.MapToSeatWithStatusVM(seat, showtimeSeats, showtimeId);
            seatsWithStatus.Add(vm);
        }

        var seatTypeVMs = seatTypes.Select(st => new SeatTypeVM
        {
            Id = st.Id,
            Name = st.Name,
            PriceMutiplier = st.PriceMutiplier,
        }).ToList();

        RoomSeatsVM roomSeatsVM = RoomSeatMapper.MapToRoomSeatVM(r, seatTypeVMs, seatsWithStatus, showtime);
        return roomSeatsVM;
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
        if (existRoom == null)
        {
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