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
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; }                 // Description of the payment

    public DateTime TransactionDateTime { get; set; }       // Date and time of the transaction

    [StringLength(50, ErrorMessage = "Payment method cannot exceed 50 characters.")]
    public string PaymentMethod { get; set; }               // e.g., Credit Card, PayPal, etc.

    [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
    public string Status { get; set; }                      // e.g., Pending, Completed, Failed

    [StringLength(5, ErrorMessage = "Currency code must be exactly 3 characters long.")]
    public string Currency { get; set; }                    // e.g., USD, VND, etc.

    // Foreign Key
    public Guid BookingId { get; set; }               

    // Navigation Properties
    public virtual Booking Booking { get; set; }

    // Navigation Collections

    // Audit Properties

}
