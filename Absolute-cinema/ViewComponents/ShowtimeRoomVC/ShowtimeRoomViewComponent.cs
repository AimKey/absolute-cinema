using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.ViewComponents.ShowtimeRoomVC
{
    public class ShowtimeRoomViewComponent : ViewComponent
    {
        private readonly IShowtimeService _showtimeService;
        private readonly IMovieService _movieService;

        public ShowtimeRoomViewComponent(
            IShowtimeService showtimeService,
            IMovieService movieService)
        {
            _showtimeService = showtimeService;
            _movieService = movieService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid movieId)
        {
            List<Showtime> movieShowtimes = _movieService.GetMovieFutureShowtime(movieId);
            return View(movieShowtimes);
        }
    }
}
