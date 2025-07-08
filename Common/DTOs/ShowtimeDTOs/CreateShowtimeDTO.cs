using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Common.DTOs.ShowtimeDTOs;

public class CreateShowtimeDTO
{
    [ValidateNever]
    public Guid Id { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required(ErrorMessage = "Please enter duration for this showtime")]
    public DateTime EndTime { get; set; }
    [Required]
    public decimal BasePrice { get; set; }
    public bool Status { get; set; } = true;

    // Foreign Key
    [Required(ErrorMessage = "Please choose a room")]
    public Guid RoomId { get; set; }
    [Required(ErrorMessage = "Please chosoe a movie")]
    public Guid MovieId { get; set; }
}
