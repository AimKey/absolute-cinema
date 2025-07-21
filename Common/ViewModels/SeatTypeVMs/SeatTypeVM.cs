using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels.SeatTypeVMs
{
    public class SeatTypeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public decimal PriceMutiplier { get; set; }
    }
}
