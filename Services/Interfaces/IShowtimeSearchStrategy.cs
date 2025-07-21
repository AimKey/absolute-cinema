using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IShowtimeSearchStrategy
    {
        List<Showtime> Search(List<Showtime> showtimes, string query);
    }
}
