﻿using Common.ViewModels;
using Common.ViewModels.UserVMs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Services.Implementations
{
    public class DashboardFacade : IDashboardFacade // Inherit from Controller
    {
        private readonly IShowtimeService _showtimeService;
        private readonly IRoomService _roomService;
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;
        private readonly ITicketService _ticketService;
        private readonly IBookingService _bookingService;

        public DashboardFacade(
            IMovieService movieService, IRoomService roomService, IShowtimeService showtimeService, IUserService userService, ITagService tagService, ITicketService ticketService, IBookingService bookingService)
        {
            _showtimeService = showtimeService;
            _movieService = movieService;
            _roomService = roomService;
            _userService = userService;
            _tagService = tagService;
            _ticketService = ticketService;
            _bookingService = bookingService;
        }

        public DashboardViewModel GetDashboardViewModel()
        {
            var viewModel = new DashboardViewModel();

            var today = DateTime.Today;
            var now = DateTime.Now;

            viewModel.TotalMovies = _movieService.GetAll().Count();

            viewModel.MoviesToday = _movieService.GetAll()
                .Where(m => m.Showtimes.Any(s => s.StartTime.Date == today))
                .Count();

            viewModel.NewUsers = _userService.GetAll()
                .Where(u => u.CreatedAt >= now.AddDays(-7))
                .Count();

            viewModel.TotalUsers = _userService.GetAll().Count();
            viewModel.TotalScreenings = _showtimeService.GetAll().Count();
            viewModel.TotalCinemaRooms = _roomService.GetAll().Count();
            viewModel.TotalGenres = _tagService.GetAll().Count();

            viewModel.TotalRevenue = _bookingService.GetAll().Sum(b => b.TotalPrice);

            viewModel.RevenueToday = _bookingService.GetAll()
                .Where(b => b.CreatedAt?.Date == today)
                .Sum(b => b.TotalPrice);

            viewModel.TotalTicketsSold = _ticketService.GetAll().Count();

            viewModel.TicketsSoldToday = _ticketService.GetAll()
                .Where(t => t.CreatedAt?.Date == today)
                .Count();

            return viewModel;
        }

        public ManageUsersVm GetManageUsersViewModel()
        {
            ManageUsersVm viewModel = new ManageUsersVm
            {
            };
            return viewModel;
        }
    }
}
