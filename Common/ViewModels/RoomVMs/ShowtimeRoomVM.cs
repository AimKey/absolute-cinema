using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.RoomVMs
{
    public class ShowtimeRoomVM
    {
        public string RoomName { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
