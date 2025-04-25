using System.ComponentModel.DataAnnotations;

namespace Web.Models.ViewModels
{
    public class ResetPasswordVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Password cannot be less than 4 chars.")]
        public string? Password1 { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Password cannot be less than 4 chars.")]
        public string? Password2 { get; set; }
    }
}
