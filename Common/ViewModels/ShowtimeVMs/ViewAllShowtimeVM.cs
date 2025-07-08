using BusinessObjects.Models;
using Common.Constants;

namespace Common.ViewModels.ShowtimeVMs
{
    public class ViewAllShowtimeVM
    {
        // Filter properties
        public string RoomName { get; set; }
        public string MovieName { get; set; }
        public bool OrderByDesc { get; set; } = true; // Default to get the newest first
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        // Pagination props
        public int PageSize { get; set; } = PageConstants.PageSize;
        public int PageNumber { get; set; } = 1;
        public int TotalPage { get; set; } = 1;

        // List of showtimes
        public IEnumerable<Showtime> Showtimes { get; set; }
    }
}
