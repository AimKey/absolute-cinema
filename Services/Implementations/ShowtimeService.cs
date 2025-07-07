using BusinessObjects.Models;
using Common.Constants;
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

        // Pagination
        var totalItems = list.Count();
        var skipItems = (vm.PageNumber - 1) * PageConstants.PageSize;
        var totalPage = (int)Math.Ceiling(totalItems * 1.0 / PageConstants.PageSize);
        list = list.Skip((vm.PageNumber - 1) * PageConstants.PageSize)
                    .Take(PageConstants.PageSize)
                    .ToList();

        // Set up the ViewModel
        vm.Showtimes = list;
        vm.PageSize = PageConstants.PageSize;
        vm.PageNumber = vm.PageNumber <= 0 ? 1 : vm.PageNumber;
        vm.TotalPage = totalPage;
        vm.RoomName = roomQuery;
        vm.MovieName = movieQuery;

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
}