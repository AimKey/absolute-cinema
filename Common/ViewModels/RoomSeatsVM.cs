using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels;

public class RoomSeatsVM
{
    public Room Room { get; set; }
    public List<SeatType> SeatTypes { get; set; }
    public List<Seat> Seats { get; set; }

    public RoomSeatsVM()
    {
        SeatTypes = new List<SeatType>();
        Seats = new List<Seat>();
    }
}
