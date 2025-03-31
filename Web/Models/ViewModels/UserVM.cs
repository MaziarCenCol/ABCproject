using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class UserVM : User
    {
        [Required]
        public string? Password2 { get; set; }
    }


}

