using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BackgroundServices.Showtimes
{
    public class ShowtimeBackgroundJob
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeBackgroundJob(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        public void ExpireOldShowtimes()
        {
            _showtimeService.ExpireOutdatedShowtimes();
        }
    }
}
