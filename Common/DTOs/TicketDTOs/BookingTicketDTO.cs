using Common.DTOs.SeatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.TicketDTOs
{
    public class BookingTicketDTO
    {
        public List<ChosenSeatDTO> ChosenSeats { get; set; }
    }
}
