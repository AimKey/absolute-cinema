using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs.RoomDTOs;

public class CreateRoomDTO
{
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(100, ErrorMessage = "Room name cannot exceed 100 characters.")]
    public string Name { get; set; }
    public int Capacity { get; set; }
    [StringLength(50, ErrorMessage = "Screen type cannot exceed 50 characters.")]
    public string ScreenType { get; set; }                      // e.g., 2D, 3D, IMAX

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }
}
