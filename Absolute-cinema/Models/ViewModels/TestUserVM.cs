using System.ComponentModel.DataAnnotations;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;

namespace Absolute_cinema.Models.ViewModels;

public class TestUserVM
{
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; } 
    // User detail
    public UserDetail UserDetail { get; set; }
}

public class UpdateProfileVM
{
    public string FullName { get; set; }
    public string Gender { get; set; }
    public DateOnly? Dob { get; set; }
    public string Phone { get; set; }
    public IFormFile? Avatar { get; set; }
    public string? AvatarURL { get; set; } // Để hiển thị preview ảnh cũ
}