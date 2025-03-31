using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.ViewModels
{
    public class RoleEdit
    {
        public Role? Role { get; set; }
        public IEnumerable<User>? Members { get; set; }

    }
}
