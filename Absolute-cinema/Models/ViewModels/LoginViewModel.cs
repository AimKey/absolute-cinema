using System.ComponentModel.DataAnnotations;

namespace Absolute_cinema.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
