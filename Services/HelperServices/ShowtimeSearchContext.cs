using BusinessObjects.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HelperServices
{
    public class ShowtimeSearchContext
    {
        private IShowtimeSearchStrategy _strategy;
        public void SetStrategy(IShowtimeSearchStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<Showtime> Search(List<Showtime> showtimes, string query)
        {
            if (_strategy == null) throw new InvalidOperationException("Search strategy is not set.");
            return _strategy.Search(showtimes, query);
        }
    }
}
