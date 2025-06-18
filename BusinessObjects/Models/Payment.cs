using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Payment
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public long OrderCode { get; set; }                     // Unique code for the order
    public int Amount { get; set; }                         // Amount to be paid
    public string Description { get; set; }                 // Description of the payment
    public DateTime TransactionDateTime { get; set; }       // Date and time of the transaction
    public string PaymentMethod { get; set; }               // e.g., Credit Card, PayPal, etc.
    public string Status { get; set; }                      // e.g., Pending, Completed, Failed
    public string Currency { get; set; }                    // e.g., USD, VND, etc.

    // Foreign Key
    public Guid BookingId { get; set; }               

    // Navigation Properties
    public Booking Booking { get; set; }

    // Navigation Collections

    // Audit Properties

}
