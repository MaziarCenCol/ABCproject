﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Entity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
