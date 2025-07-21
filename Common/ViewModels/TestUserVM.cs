using System.ComponentModel.DataAnnotations;
using BusinessObjects.Models;

namespace Common.ViewModels;

public class TestUserVM
{
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    public string Role { get; set; } 
    // User detail
    public UserDetail UserDetail { get; set; }
}