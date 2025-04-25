using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Password cannot be less than 4 chars.")]
        public string? Password { get; set; }

        [NotMapped]
        public string? Token { get; set; }
    }

    public enum Role
    {
        Admin,
        Manager,
        Technician
    }

}
