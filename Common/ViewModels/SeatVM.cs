using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ViewModels;

public class SeatVM
{
    public Guid Id { get; set; }
    public string SeatRow { get; set; }                             // e.g., A, B, C, etc.
    public int SeatNumber { get; set; }                             // e.g., 1, 2, 3, etc.
    public string Description { get; set; }
    public Guid SeatTypeId { get; set; }
    public Guid RoomId { get; set; }
}
