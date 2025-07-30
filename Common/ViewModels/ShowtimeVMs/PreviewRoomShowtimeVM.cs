using System;
using BusinessObjects.Models;

namespace Common.ViewModels.ShowtimeVMs;

public class PreviewRoomShowtimeVM
{
    // The start and end time of the showtime
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    // The movie information associated with the showtime
    public Movie Movie { get; set; }
}
