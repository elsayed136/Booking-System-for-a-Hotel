﻿using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Location { get; set; }
    }
}
