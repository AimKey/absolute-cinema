

using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public decimal Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
