using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Admin;

public class AdminController : Controller
{
    private readonly IShowtimeService _showtimeService;
    private readonly IRoomService _roomService;
    private readonly IMovieService _movieService;
    private readonly IUserService _userService;
    private readonly ITagService _tagService;
    private readonly ITicketService _ticketService;
    private readonly IBookingService _bookingService;

    public AdminController(IMovieService movieService, IRoomService roomService, IShowtimeService showtimeService, IUserService userService, ITagService tagService, ITicketService ticketService, IBookingService bookingService)
    {
        _showtimeService = showtimeService;
        _movieService = movieService;
        _roomService = roomService;
        _userService = userService;
        _tagService = tagService;
        _ticketService = ticketService;
        _bookingService = bookingService;
    }
    public IActionResult Index()
    {
        var viewModel = new DashboardViewModel();

        viewModel.TotalMovies = _movieService.GetAll().Count();
        viewModel.MoviesToday = _movieService.GetAll().Where(m => m.Showtimes.Any(s => s.StartTime.Equals(DateTime.Now))).Count();
        viewModel.NewUsers = _userService.GetAll().Where(u => u.CreatedAt >= DateTime.UtcNow.AddDays(-7)).Count();
        viewModel.TotalUsers = _userService.GetAll().Count();
        viewModel.TotalScreenings = _showtimeService.GetAll().Count();
        viewModel.TotalCinemaRooms = _roomService.GetAll().Count();
        viewModel.TotalGenres = _tagService.GetAll().Count();

        viewModel.TotalRevenue = _bookingService.GetAll().Sum(b => b.TotalPrice); ;
        viewModel.RevenueToday = _bookingService.GetAll().Where(b => b.CreatedAt.Equals(DateTime.Now)).Sum(b => b.TotalPrice);    
        viewModel.TotalTicketsSold = _ticketService.GetAll().Count();
        viewModel.TicketsSoldToday = _ticketService.GetAll().Where(t => t.CreatedAt.Equals(DateTime.Now)).Count();



        return View(viewModel);
    }

}
