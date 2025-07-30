using BusinessObjects.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class SearchShowtimeByRoomNameStrategy : IShowtimeSearchStrategy
    {
        public List<Showtime> Search(List<Showtime> showtimes, string query)
        {
            return showtimes
                .Where(st => st.Room.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
