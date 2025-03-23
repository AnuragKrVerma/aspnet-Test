using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTest3.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<EmployeeLanguage> EmployeeLanguages { get; set; }
    }
}