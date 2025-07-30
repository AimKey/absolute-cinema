using BusinessObjects.Models;

namespace Common.ViewModels.BookingVMs;

public class BookingViewModel
{
    public Booking Booking { get; set; }
    public bool HasFeedback { get; set; }
    public Guid? MovieId { get; set; } // Để dễ dàng truyền MovieId vào các hàm JavaScript
}
