﻿using System.ComponentModel.DataAnnotations;

namespace Project_Test.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Phone{ get; set; }
    }
}
