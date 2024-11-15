using System.ComponentModel.DataAnnotations;

namespace ApiCarro.ViewModels
{
    public class UserReturnViewModel
    {
        public int Id { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
    public class UserLoginViewModel
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    public class UserSignupViewModel
    {
        [Required]
        public string Usuario { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
