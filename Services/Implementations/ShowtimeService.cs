using BusinessObjects.Models;
using Common.Constants;
using Common.DTOs.ShowtimeDTOs;
using Common.ViewModels.ShowtimeVMs;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class ShowtimeService : IShowtimeService
{
    private readonly IShowtimeRepository _showtimeRepository;

    public ShowtimeService(IShowtimeRepository showtimeRepository)
    {
        _showtimeRepository = showtimeRepository;
    }

    public ViewAllShowtimeVM GetAllVM(ViewAllShowtimeVM vm = null)
    {
        var roomQuery = string.IsNullOrEmpty(vm?.RoomName) ? string.Empty : vm.RoomName;
        var movieQuery = string.IsNullOrEmpty(vm?.MovieName) ? string.Empty : vm.MovieName;
        // Search query filters
        var list = _showtimeRepository.Get(
            st => st.Room.Name.Contains(roomQuery)
            && st.Movie.Title.Contains(movieQuery));

        var startTime = vm.FromDate ?? DateTime.MinValue;
        var endTime = vm.ToDate ?? DateTime.MaxValue;
        // Filter by date range
        list = list.Where(st =>
            st.StartTime <= endTime && st.EndTime >= startTime
        ).ToList();

        // Order by
        if (vm.OrderByDesc)
        {
            list = list.OrderByDescending(st => st.StartTime).ToList();
        }
        else
        {
            list = list.OrderBy(st => st.StartTime).ToList();
        }

        // Pagination
        var totalItems = list.Count();
        var totalPage = (int)Math.Ceiling(totalItems * 1.0 / PageConstants.PageSize);
        var skipItems = (vm.PageNumber - 1) * PageConstants.PageSize;
        list = list.Skip((vm.PageNumber - 1) * PageConstants.PageSize)
                    .Take(PageConstants.PageSize)
                    .ToList();

        // Set up the ViewModel
        vm.PageSize = PageConstants.PageSize;
        vm.PageNumber = vm.PageNumber <= 0 ? 1 : vm.PageNumber;
        vm.TotalPage = totalPage;
        vm.RoomName = roomQuery;
        vm.MovieName = movieQuery;
        vm.Showtimes = list;
        return vm;
    }


    public IEnumerable<Showtime> GetAll()
    {
        return _showtimeRepository.Get();
    }

    public Showtime GetById(Guid id)
    {
        return _showtimeRepository.GetByID(id);
    }

    public void Add(Showtime showtime)
    {
        _showtimeRepository.Insert(showtime);
        _showtimeRepository.Save();
    }

    public void Update(Showtime showtime)
    {
        _showtimeRepository.Update(showtime);
        _showtimeRepository.Save();
    }

    public void Delete(Guid id)
    {
        _showtimeRepository.Delete(id);
        _showtimeRepository.Save();
    }

    public void Delete(Showtime showtime)
    {
        _showtimeRepository.Delete(showtime);
        _showtimeRepository.Save();
    }

    public List<PreviewRoomShowtimeVM> GetCurrentShowtimeOfRoomInADay(Guid roomId, DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        var showtimes = _showtimeRepository.Get(
            st => st.RoomId == roomId &&
                  st.StartTime >= dayStart &&
                  st.EndTime < dayEnd);
        List<PreviewRoomShowtimeVM> vms = new();
        foreach (var showtime in showtimes)
        {
            var vm = new PreviewRoomShowtimeVM
            {
                StartTime = showtime.StartTime,
                EndTime = showtime.EndTime,
                Movie = showtime.Movie,
            };
            vms.Add(vm);
        }
        vms = vms.OrderBy(vm => vm.StartTime).ToList();
        return vms;
    }

    public void AddShowtimeDTO(CreateShowtimeDTO createShowtimeDTO)
    {
        var showtimes = _showtimeRepository.Get(s =>
            s.RoomId == createShowtimeDTO.RoomId &&
            s.StartTime < createShowtimeDTO.EndTime &&
            createShowtimeDTO.StartTime < s.EndTime
        ); 
        
        if (showtimes.Any())
        {
            throw new Exception($"Time: {createShowtimeDTO.StartTime.ToString("hh:mm tt")}" +
            $" - {createShowtimeDTO.EndTime.ToString("hh: mm tt")} is already booked by other showtime");
        }

        // Map to normal dto
        Showtime s = new Showtime
        {
            Id = Guid.NewGuid(),
            StartTime = createShowtimeDTO.StartTime,
            EndTime = createShowtimeDTO.EndTime,
            BasePrice = createShowtimeDTO.BasePrice,
            CreatedAt = DateTime.Now,
            MovieId = createShowtimeDTO.MovieId,
            RoomId = createShowtimeDTO.RoomId,
            Status = createShowtimeDTO.Status,
        };
        Add(s);
    }
}