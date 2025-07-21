using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Absolute_cinema.ViewComponents.ShowtimeVC
{
    public class ShowtimesOfMovieInDateViewComponent : ViewComponent
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimesOfMovieInDateViewComponent(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid movieId, DateTime date)
        {
            var showtimes = _showtimeService.GetAllShowtimesOfAMovieInDate(movieId, date);
            return View(showtimes);
        }
    }
}
