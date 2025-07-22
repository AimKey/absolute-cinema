using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.PaymentDTOs
{
    public class VNPayPaymentDTO
    {
        public Guid BookingId { get; set; }
        public Guid UserId { get; set; }
        public int TicketsCount { get; set; }
        public string MovieTitle { get; set; }
        public decimal Amount { get; set; }
    }
}
