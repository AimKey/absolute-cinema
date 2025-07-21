using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.BookingVMs
{
    public class ReviewBookingVM
    {
        public Guid BookingId { get; set; }
        public Showtime Showtime { get; set; }
        public List<Seat> BookedSeat { get; set; }
        public List<Ticket> Tickets { get; set; }
        public decimal TotalBookingCost { get; set; }
        public Payment Payment { get; set; } = null;
        public User UserInfo { get; set; }
    }
}
