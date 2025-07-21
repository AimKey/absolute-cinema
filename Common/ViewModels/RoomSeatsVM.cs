using BusinessObjects.Models;
using Common.ViewModels.SeatTypeVMs;
using Common.ViewModels.SeatVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels;

public class RoomSeatsVM
{
    public Room Room { get; set; }
    public Guid ShowtimeId { get; set; }
    public List<SeatTypeVM> SeatTypes { get; set; }
    public List<SeatWithStatusVM> Seats { get; set; }
    public decimal TicketBasePrice { get; set; }
}
