using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest3.Models
{
    public class Position
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<Employee> Employees { get; set; }
    }
}